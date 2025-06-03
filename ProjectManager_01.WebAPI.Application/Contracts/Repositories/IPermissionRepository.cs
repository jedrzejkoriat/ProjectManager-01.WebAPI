using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<bool> DeleteAsync(Guid permissionId, IDbTransaction transaction);
}
