using System.Data;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectRolePermissionService
{
    Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto);
    Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId);
    Task<IEnumerable<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync();
    Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId);
    Task DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction);
    Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto, IDbTransaction transaction);
}
