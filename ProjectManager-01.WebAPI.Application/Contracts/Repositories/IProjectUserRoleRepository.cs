using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IProjectUserRoleRepository : 
    ICreateable<ProjectUserRole>, IReadable<ProjectUserRole>, IUpdateable<ProjectUserRole>, IDeleteable
{
    Task<List<ProjectUserRole>> GetByUserIdAsync(Guid userId);
    Task<List<ProjectUserRole>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId);
}
