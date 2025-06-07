using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> SoftDeleteByIdAsync(Guid id);
    Task<bool> DeleteByIdAsync(Guid id, IDbTransaction transaction);
    Task<bool> CreateAsync(User entity, IDbTransaction transaction);
    Task<IEnumerable<User>> GetAllByProjectIdAsync(Guid projectId);
    Task<User> GetByUserNameAsync(string userName);
    Task<User> GetUserClaimsByIdAsync(Guid userId);
}
