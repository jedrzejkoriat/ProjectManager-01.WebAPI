using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IDbConnection dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<List<User>> GetByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT DISTINCT u.* FROM Users u
                    JOIN ProjectUserRoles pur ON u.Id = pur.UserId
                    WHERE pur.ProjectId = @ProjectId
                    AND u.IsDeleted = 0";
        var result = await dbConnection.QueryAsync<User>(sql, new { ProjectId = projectId });

        return result.ToList();
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var sql = @"UPDATE Users
                    SET IsDeleted = 1
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(User entity)
    {
        var sql = @"INSERT INTO Users (Id, UserName, Email, PasswordHash, CreatedAt)
                    VALUES (@Id, @UserName, @Email, @PasswordHash, @CreatedAt)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating user failed.");
    }

    public async Task<List<User>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Users";
        var result = await dbConnection.QueryAsync<User>(sql);

        return result.ToList();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Users WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<User>(sql, new { Id = id });

        return result;
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        var sql = @"UPDATE USers
                    SET UserName = @UserName,
                        Email = @Email,
                        PasswordHash = @PasswordHash
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Users WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
