using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IUserRoleRepository
{
    Task<bool> CreateAsync(UserRole userRole);
    Task<UserRole> GetByUserIdAsync(Guid userId);
    Task<bool> DeleteAsync(Guid userId);
    Task<List<UserRole>> GetByRoleIdAsync(Guid roleId);
}
