using System.Data;
using System.Transactions;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectRolePermissionService
{
    Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto);
    Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId);
    Task<List<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync();
    Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId);
    Task DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction);
}
