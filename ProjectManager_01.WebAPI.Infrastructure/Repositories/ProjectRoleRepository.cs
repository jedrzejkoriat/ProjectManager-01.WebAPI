using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectRoleRepository : IProjectRoleRepository
{
    private readonly IDbConnection dbConnection;

    public ProjectRoleRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }
    // ============================= QUERIES =============================
    public async Task<List<ProjectRole>> GetAllAsync()
    {
        var sql = @"SELECT pr.*, p.*
                    FROM ProjectRoles pr
                    JOIN ProjectRolePermissions prp ON pr.Id = prp.ProjectRoleId
                    JOIN Permissions p ON prp.PermissionId = p.Id";

        var projectRoleDict = new Dictionary<Guid, ProjectRole>();

        var result = await dbConnection.QueryAsync<ProjectRole, Permission, ProjectRole>(
            sql,
            (projectRole, permission) =>
            {
                if (!projectRoleDict.TryGetValue(projectRole.Id, out var pr))
                {
                    pr = projectRole;
                    pr.Permissions = new List<Permission>();
                    projectRoleDict[pr.Id] = pr;
                }

                pr.Permissions.Add(permission);
                return pr;
            },
            splitOn: "Id"
        );

        return projectRoleDict.Values.ToList();
    }

    public async Task<ProjectRole> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT pr.*, p.*
                    FROM ProjectRoles pr
                    JOIN ProjectRolePermissions prp ON pr.Id = prp.ProjectRoleId
                    JOIN Permissions p ON prp.PermissionId = p.Id
                    WHERE pr.Id = @Id";

        var projectRoleDict = new Dictionary<Guid, ProjectRole>();

        var result = await dbConnection.QueryAsync<ProjectRole, Permission, ProjectRole>(
            sql,
            (projectRole, permission) =>
            {
                if (!projectRoleDict.TryGetValue(projectRole.Id, out var pr))
                {
                    pr = projectRole;
                    pr.Permissions = new List<Permission>();
                    projectRoleDict[pr.Id] = pr;
                }

                pr.Permissions.Add(permission);
                return pr;
            },
            new { Id = id },
            splitOn: "Id"
        );

        return projectRoleDict.Values.FirstOrDefault();
    }

    // ============================= COMMANDS =============================
    public async Task<Guid> CreateAsync(ProjectRole entity, IDbTransaction transaction)
    {
        var sql = @"INSERT INTO ProjectRoles (Id, ProjectId, Name)
                    VALUES (@Id, @ProjectId, @Name)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity, transaction);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ProjectRole failed");
    }

    public Task<bool> DeleteAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRoles 
                    WHERE Id = @Id";
        var result = dbConnection.ExecuteAsync(sql, new { Id = projectRoleId }, transaction);
        return result.ContinueWith(t => t.Result > 0);
    }

    public async Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRoles
                    WHERE ProjectId = @ProjectId";
        var result = await dbConnection.ExecuteAsync(sql, new { ProjectId = projectId }, transaction);

        return result > 0;
    }

    public async Task<Guid> CreateAsync(ProjectRole entity)
    {
        var sql = @"INSERT INTO ProjectRoles (Id, ProjectId, Name)
                    VALUES (@Id, @ProjectId, @Name)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ProjectRole failed");
    }

    public async Task<bool> UpdateAsync(ProjectRole entity)
    {
        var sql = @"UPDATE ProjectRoles
                    SET ProjectId = @ProjectId,
                        Name = @Name
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM ProjectRoles 
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
