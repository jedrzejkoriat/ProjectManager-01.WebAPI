using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class PermissionRepository : IPermissionRepository
{
    private readonly IDbConnection _dbConnection;

    public PermissionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        var sql = "SELECT * FROM Permissions";
        var result = await _dbConnection.QueryAsync<Permission>(sql);

        return result.ToList();
    }

    public async Task<Permission> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Permissions 
                        WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstOrDefaultAsync<Permission>(sql, new { Id = id });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> CreateAsync(Permission permission)
    {
        var sql = @"INSERT INTO Permissions (Id, Name) 
                    VALUES (@Id, @Name)";
        var result = await _dbConnection.ExecuteAsync(sql, permission);

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Permission entity)
    {
        var sql = @"UPDATE Permissions 
                        SET Name = @Name 
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Permissions 
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid permissionId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Permissions 
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = permissionId }, transaction);

        return result > 0;
    }
}
