using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class UserRoleRepository : IUserRoleRepository
{

    private readonly IDbConnection dbConnection;

    public UserRoleRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<bool> DeleteByRoleIdAsync(Guid roleId, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM UserRoles 
                    WHERE RoleId = @RoleId";
        var result = await connection.ExecuteAsync(sql, new { RoleId = roleId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM UserRoles 
                    WHERE UserId = @UserId";
        var result = await connection.ExecuteAsync(sql, new { UserId = userId }, transaction);

        return result > 0;
    }

    public async Task<List<UserRole>> GetByRoleIdAsync(Guid roleId)
    {
        var sql = @"SELECT * FROM UserRoles 
                    WHERE RoleId = @RoleId";
        var result = await dbConnection.QueryAsync<UserRole>(sql, new { RoleId = roleId });

        return result.ToList();
    }

    // ============================= CRUD =============================
    public async Task<bool> CreateAsync(UserRole entity)
    {
        var sql = @"INSERT INTO UserRoles (UserId, RoleId)
                    VALUES (@UserId, @RoleId)";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<UserRole> GetByUserIdAsync(Guid userId)
    {
        var sql = @"SELECT * FROM UserRoles 
                    WHERE UserId = @UserId";
        var result = await dbConnection.QueryFirstAsync<UserRole>(sql, new { UserId = userId });

        return result;
    }

    public async Task<List<UserRole>> GetAllAsync()
    {
        var sql = @"SELECT * FROM UserRoles";
        var result = await dbConnection.QueryAsync<UserRole>(sql);

        return result.ToList();
    }

    public async Task<bool> UpdateAsync(UserRole userRole)
    {
        var sql = @"UPDATE UserRoles
                    SET RoleId = @RoleId
                    WHERE UserId = @UserId";
        var result = await dbConnection.ExecuteAsync(sql, userRole);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid userId)
    {
        var sql = @"DELETE FROM UserRoles 
                    WHERE UserId = @UserId";
        var result = await dbConnection.ExecuteAsync(sql, new { UserId = userId });

        return result > 0;
    }
}
