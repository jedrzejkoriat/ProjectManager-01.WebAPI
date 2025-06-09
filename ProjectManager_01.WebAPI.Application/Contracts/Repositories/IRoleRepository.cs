using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> DeleteByIdAsync(Guid Id, IDbTransaction transaction);
}
