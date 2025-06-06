using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> SoftDeleteAsync(Guid id);
    Task<bool> DeleteAsync(Guid id, IDbTransaction transaction);
    Task<Guid> CreateAsync(User entity, IDbTransaction transaction);
    Task<IEnumerable<User>> GetByProjectIdAsync(Guid projectId);
}
