using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<bool> DeleteByIdAsync(Guid permissionId, IDbTransaction transaction);
}
