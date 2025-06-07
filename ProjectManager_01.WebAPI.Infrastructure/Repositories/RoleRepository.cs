using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly IDbConnection _dbConnection;

    public RoleRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Roles";
        var result = await _dbConnection.QueryAsync<Role>(sql);

        return result.ToList();
    }

    public async Task<Role> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Roles 
                    WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstAsync<Role>(sql, new { Id = id });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> DeleteByIdAsync(Guid Id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Roles 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = Id }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Roles 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> CreateAsync(Role entity)
    {
        var sql = @"INSERT INTO Roles (Id, Name)
					VALUES (@Id, @Name)";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Role entity)
    {
        var sql = @"UPDATE Roles
					SET Name = @Name
					WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }
}
