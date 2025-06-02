using ProjectManager_01.Application.DTOs.ProjectRolePermissions;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectRolePermissionService
{
    Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto);
    Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId);
    Task<List<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync();
    Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId);
}
