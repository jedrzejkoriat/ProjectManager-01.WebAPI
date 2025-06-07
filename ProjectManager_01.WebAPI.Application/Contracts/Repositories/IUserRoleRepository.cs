using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IUserRoleRepository
{
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task<bool> CreateAsync(UserRole userRole);
    Task<UserRole> GetByUserIdAsync(Guid userId);
    Task<bool> DeleteByIdAsync(Guid userId);
    Task<bool> UpdateAsync(UserRole userRole);
    Task<bool> DeleteAllByRoleIdAsync(Guid roleId, IDbTransaction transaction);
    Task<bool> DeleteAllByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> CreateAsync(UserRole userRole, IDbTransaction transaction);
}
