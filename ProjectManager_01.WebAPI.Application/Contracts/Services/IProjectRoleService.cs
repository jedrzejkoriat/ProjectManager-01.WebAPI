using ProjectManager_01.Application.DTOs.ProjectRoles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectRoleService
{
    Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto);
    Task UpdateProjectRoleAsync(ProjectRoleUpdateDto projectRoleUpdateDto);
    Task DeleteProjectRoleAsync(Guid projectRoleId);
    Task<ProjectRoleDto> GetProjectRoleByIdAsync(Guid projectRoleId);
    Task<List<ProjectRoleDto>> GetAllProjectRolesAsync();
}
