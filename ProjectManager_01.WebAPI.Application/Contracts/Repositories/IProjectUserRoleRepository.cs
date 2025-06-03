using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectUserRoleRepository : IRepository<ProjectUserRole>
{
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
}
