using System.Data;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectRoleService
{
    Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto);
    Task UpdateProjectRoleAsync(ProjectRoleUpdateDto projectRoleUpdateDto);
    Task DeleteProjectRoleAsync(Guid projectRoleId);
    Task<ProjectRoleDto> GetProjectRoleByIdAsync(Guid projectRoleId);
    Task<List<ProjectRoleDto>> GetAllProjectRolesAsync();
    Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
}
