using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IPermissionRepository : IGenericRepository<Permission>
{
    Task<bool> DeleteAsync(Guid permissionId, IDbConnection connection, IDbTransaction transaction);
}
