using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectUserRoleRepository : IRepository<ProjectUserRole>
{
    Task<List<ProjectUserRole>> GetByUserIdAsync(Guid userId);
    Task<List<ProjectUserRole>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId);
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
}
