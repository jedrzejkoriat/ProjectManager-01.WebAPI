using ProjectManager_01.Application.DTOs.ProjectUserRoles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectUserRoleService
{
    Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto);
    Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto);
    Task DeleteProjectUserRoleAsync(Guid projectUserRoleId);
    Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId);
    Task<List<ProjectUserRoleDto>> GetAllProjectUserRolesAsync();
}
