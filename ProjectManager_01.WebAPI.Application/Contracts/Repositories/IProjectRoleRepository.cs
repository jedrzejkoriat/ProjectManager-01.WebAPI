using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRoleRepository : IRepository<ProjectRole>
{
    Task<bool> DeleteAsync(Guid projectRoleId, IDbTransaction transaction);
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<Guid> CreateAsync(ProjectRole entity, IDbTransaction transaction);
}
