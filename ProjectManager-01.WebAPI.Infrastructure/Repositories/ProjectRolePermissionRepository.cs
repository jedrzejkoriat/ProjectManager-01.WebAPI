using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public async Task<bool> CreateAsync(ProjectRolePermission projectRolePermissions)
    {
        var sql = @"INSERT INTO ProjectRolePermissions (ProjectRoleId, PermissionId)
                    VALUES (@ProjectRoleId, PermissionId)";
        var result = await dbConnection.ExecuteAsync(sql, projectRolePermissions);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid projectRoleId, Guid permissionId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions
                    WHERE ProjectRoleId = @ProjectRoleId AND PermissionId = @PermissionId";
        var result = await dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId, PermissionId = permissionId });

        return result > 0;
    }

    public async Task<bool> DeleteByPermissionIdAsync(Guid permissionId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions WHERE PermissionId = @PermissionId";
        var result = await dbConnection.ExecuteAsync(sql, new { PermissionId = permissionId });

        return result > 0;
    }

    public async Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId)
    {
        var sql = @"DELETE FROM ProjectRolePermissions WHERE ProjectRoleId = @ProjectRoleId";
        var result = await dbConnection.ExecuteAsync(sql, new {ProjectRoleId = projectRoleId});

        return result > 0;
    }
}
