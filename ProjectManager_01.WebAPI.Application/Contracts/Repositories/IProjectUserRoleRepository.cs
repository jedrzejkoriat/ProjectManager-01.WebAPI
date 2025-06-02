using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectUserRoleRepository : IGenericRepository<ProjectUserRole>
{
    Task<List<ProjectUserRole>> GetByUserIdAsync(Guid userId);
    Task<List<ProjectUserRole>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId);
}
