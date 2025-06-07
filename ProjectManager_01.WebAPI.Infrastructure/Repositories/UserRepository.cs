using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Users";
        var result = await _dbConnection.QueryAsync<User>(sql);

        return result.ToList();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Users 
                    WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstAsync<User>(sql, new { Id = id });

        return result;
    }

    public async Task<IEnumerable<User>> GetAllByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT DISTINCT u.*
                    FROM Users u
                    INNER JOIN ProjectUserRole pur ON pur.UserId = u.Id
                    WHERE pur.ProjectId = @ProjectId;";
        var result = await _dbConnection.QueryAsync<User>(sql, new { ProjectId = projectId });

        return result.ToList();
    }

    public async Task<User> GetUserClaimsByIdAsync(Guid userId)
    {
        var sql = @"SELECT 
                        u.*, 
                        r.Id, r.Name,
                        pr.Id, pr.ProjectId, pr.Name,
                        prp.ProjectRoleId, prp.PermissionId,
                        p.Id, p.Name
                    FROM Users u
                    LEFT JOIN UserRoles ur ON ur.UserId = u.Id
                    LEFT JOIN Roles r ON ur.RoleId = r.Id
                    LEFT JOIN ProjectUserRoles pur ON pur.UserId = u.Id
                    LEFT JOIN ProjectRoles pr ON pur.ProjectRoleId = pr.Id
                    LEFT JOIN ProjectRolePermissions prp ON pr.Id = prp.ProjectRoleId
                    LEFT JOIN Permissions p ON prp.PermissionId = p.Id
                    WHERE u.Id = @UserId";

        var userDict = new Dictionary<Guid, User>();
        var projectRoleDict = new Dictionary<Guid, ProjectRole>();

        var result = await _dbConnection.QueryAsync<User, Role, ProjectRole, Permission, User>(
            sql,
            (user, role, projectRole, permission) =>
            {
                if (!userDict.TryGetValue(user.Id, out var userEntry))
                {
                    userEntry = user;
                    userEntry.Role = role;
                    userEntry.ProjectRoles = new List<ProjectRole>();
                    userDict.Add(userEntry.Id, userEntry);
                }

                if (projectRole != null)
                {
                    if (!projectRoleDict.TryGetValue(projectRole.Id, out var prEntry))
                    {
                        prEntry = projectRole;
                        prEntry.Permissions = new List<Permission>();
                        projectRoleDict.Add(prEntry.Id, prEntry);

                        ((List<ProjectRole>)userEntry.ProjectRoles).Add(prEntry);
                    }

                    if (permission != null && !prEntry.Permissions.Any(p => p.Id == permission.Id))
                    {
                        prEntry.Permissions.Add(permission);
                    }
                }

                return userEntry;
            },
            new { UserId = userId },
            splitOn: "Id,Id,Id"
        );

        return userDict.Values.FirstOrDefault();
    }

    public async Task<User> GetByUserNameAsync(string userName)
    {
        var sql = @"SELECT * FROM Users
                    WHERE UserName = @UserName";
        var result = await _dbConnection.QueryFirstAsync<User>(sql, new { UserName = userName });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> CreateAsync(User entity, IDbTransaction transaction)
    {
        var sql = @"INSERT INTO Users (Id, UserName, Email, PasswordHash, CreatedAt)
                    VALUES (@Id, @UserName, @Email, @PasswordHash, @CreatedAt)";
        var result = await _dbConnection.ExecuteAsync(sql, entity, transaction);

        return result > 0;
    }

    public async Task<bool> CreateAsync(User entity)
    {
        var sql = @"INSERT INTO Users (Id, UserName, Email, PasswordHash, CreatedAt)
                    VALUES (@Id, @UserName, @Email, @PasswordHash, @CreatedAt)";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        var sql = @"UPDATE Users
                    SET UserName = @UserName,
                        Email = @Email,
                        PasswordHash = @PasswordHash
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Users 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Users 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }

    public async Task<bool> SoftDeleteByIdAsync(Guid id)
    {
        var sql = @"UPDATE Users
                    SET IsDeleted = 1
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
