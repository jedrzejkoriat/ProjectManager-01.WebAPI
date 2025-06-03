using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class PermissionRepository : IPermissionRepository
{
    private readonly IDbConnection dbConnection;

    public PermissionRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<List<Permission>> GetAllAsync()
    {
        var sql = "SELECT * FROM Permissions";
        var result = await dbConnection.QueryAsync<Permission>(sql);

        return result.ToList();
    }

    public async Task<Permission> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Permissions 
                        WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync(sql, new { Id = id });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<Guid> CreateAsync(Permission permission)
    {
        var sql = @"INSERT INTO Permissions (Id, Name) 
                    VALUES (@Id, @Name)";
        permission.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, permission);

        if (result > 0)
            return permission.Id;
        else
            throw new Exception("Insert to permissions failed");
    }

    public async Task<bool> UpdateAsync(Permission entity)
    {
        var sql = @"UPDATE Permissions 
                        SET Name = @Name 
                        WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Permissions 
                        WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid permissionId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Permissions 
                        WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = permissionId }, transaction);

        return result > 0;
    }
}
