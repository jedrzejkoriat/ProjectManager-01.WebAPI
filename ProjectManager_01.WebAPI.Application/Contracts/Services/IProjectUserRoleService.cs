using System.Data;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectUserRoleService
{
    Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto);
    Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto);
    Task DeleteProjectUserRoleAsync(Guid projectUserRoleId);
    Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId);
    Task<IEnumerable<ProjectUserRoleDto>> GetAllProjectUserRolesAsync();
    Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task DeleteByProjectRoleId(Guid projectRoleId, IDbTransaction transaction);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<IEnumerable<ProjectUserRoleDto>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId);
}
