using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectRolePermissionRepository : IProjectRolePermissionRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRolePermissionRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<ProjectRolePermission>> GetAllAsync()
    {
        var sql = @"SELECT * FROM ProjectRolePermissions";
        var result = await _dbConnection.QueryAsync<ProjectRolePermission>(sql);

        return result.ToList();
    }

    public async Task<ProjectRolePermission> GetByIdAsync(Guid projectRoleId, Guid permissionId)
    {
        var sql = @"SELECT * FROM ProjectRolePermissions
                    WHERE ProjectRoleId = @ProjectRoleId 
                    AND PermissionId = @PermissionId";
        var result = await _dbConnection.QueryFirstAsync<ProjectRolePermission>(sql, new { RoleId = projectRoleId, PermissionId = permissionId });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> CreateAsync(ProjectRolePermission projectRolePermission, IDbTransaction transaction)
    {
        var sql = @"INSERT INTO ProjectRolePermissions (ProjectRoleId, PermissionId)
                    VALUES (@ProjectRoleId, @PermissionId)";
        var result = await _dbConnection.ExecuteAsync(sql, projectRolePermission, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAllByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE ProjectRoleId = @ProjectRoleId";
        var result = await _dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId }, transaction);

        return result > 0;
    }

    public async Task<bool> CreateAsync(ProjectRolePermission projectRolePermission)
    {
        var sql = @"INSERT INTO ProjectRolePermissions (ProjectRoleId, PermissionId)
                    VALUES (@ProjectRoleId, PermissionId)";
        var result = await _dbConnection.ExecuteAsync(sql, projectRolePermission);

        return result > 0;
    }

    public async Task<bool> DeleteByProjectRoleIdAndPermissionIdAsync(Guid projectRoleId, Guid permissionId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions
                    WHERE ProjectRoleId = @ProjectRoleId AND PermissionId = @PermissionId";
        var result = await _dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId, PermissionId = permissionId });

        return result > 0;
    }

    public async Task<bool> DeleteAllByPermissionIdAsync(Guid permissionId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectRolePermissions 
                    WHERE PermissionId = @PermissionId";
        var result = await _dbConnection.ExecuteAsync(sql, new { PermissionId = permissionId }, transaction);

        return result > 0;
    }
}
