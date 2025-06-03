using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<bool> DeleteAsync(Guid Id, IDbConnection connection, IDbTransaction transaction);
}
