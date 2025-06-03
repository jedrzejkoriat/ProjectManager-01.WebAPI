using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectRolePermissionRepository : IProjectRolePermissionRepository
{
    private readonly IDbConnection dbConnection;

    public ProjectRolePermissionRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<bool> DeleteByPermissionIdAsync(Guid permissionId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE PermissionId = @PermissionId";
        var result = await dbConnection.ExecuteAsync(sql, new { PermissionId = permissionId });

        return result > 0;
    }

    public async Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE ProjectRoleId = @ProjectRoleId";
        var result = await dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId });

        return result > 0;
    }

    public async Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE ProjectRoleId = @ProjectRoleId";
        var result = await connection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId }, transaction);

        return result > 0;
    }

    // ============================= CRUD =============================
    public async Task<bool> CreateAsync(ProjectRolePermission projectRolePermissions)
    {
        var sql = @"INSERT INTO ProjectRolePermissions (ProjectRoleId, PermissionId)
                    VALUES (@ProjectRoleId, PermissionId)";
        var result = await dbConnection.ExecuteAsync(sql, projectRolePermissions);

        return result > 0;
    }

    public async Task<List<ProjectRolePermission>> GetAllAsync()
    {
        var sql = @"SELECT * FROM ProjectRolePermissions";
        var result = await dbConnection.QueryAsync<ProjectRolePermission>(sql);

        return result.ToList();
    }

    public async Task<ProjectRolePermission> GetByIdAsync(Guid projectRoleId, Guid permissionId)
    {
        var sql = @"SELECT * FROM ProjectRolePermissions
                    WHERE ProjectRoleId = @ProjectRoleId 
                    AND PermissionId = @PermissionId";
        var result = await dbConnection.QueryFirstAsync<ProjectRolePermission>(sql, new { RoleId = projectRoleId, PermissionId = permissionId });

        return result;
    }

    public async Task<bool> DeleteAsync(Guid projectRoleId, Guid permissionId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions
                    WHERE ProjectRoleId = @ProjectRoleId AND PermissionId = @PermissionId";
        var result = await dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId, PermissionId = permissionId });

        return result > 0;
    }

    public async Task<bool> DeleteByPermissionIdAsync(Guid permissionId, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE PermissionId = @PermissionId";
        var result = await dbConnection.ExecuteAsync(sql, new { PermissionId = permissionId }, transaction);

        return result > 0;
    }
}
