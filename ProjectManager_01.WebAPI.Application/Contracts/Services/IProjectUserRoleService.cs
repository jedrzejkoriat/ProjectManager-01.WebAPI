using System.Data;
using System.Transactions;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectUserRoleService
{
    Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto);
    Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto);
    Task DeleteProjectUserRoleAsync(Guid projectUserRoleId);
    Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId);
    Task<List<ProjectUserRoleDto>> GetAllProjectUserRolesAsync();
    Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task DeleteByProjectRoleId(Guid projectRoleId, IDbTransaction transaction);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
}
