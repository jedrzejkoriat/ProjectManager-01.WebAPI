using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRoleRepository : IRepository<ProjectRole>
{
    Task<bool> DeleteByIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task<bool> DeleteAllByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<bool> CreateAsync(ProjectRole entity, IDbTransaction transaction);
}
