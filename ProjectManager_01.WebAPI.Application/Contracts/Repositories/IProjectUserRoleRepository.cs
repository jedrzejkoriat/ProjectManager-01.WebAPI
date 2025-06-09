using System.Data;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectUserRoleRepository : IRepository<ProjectUserRole>
{
    Task<bool> DeleteAllByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<bool> DeleteAllByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<IEnumerable<ProjectUserRole>> GetAllByUserIdAndProjectIdAsync(Guid userId, Guid projectId);
}
