using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<User>> GetByProjectIdAsync(Guid projectId);
    Task<bool> SoftDeleteAsync(Guid id);
}
