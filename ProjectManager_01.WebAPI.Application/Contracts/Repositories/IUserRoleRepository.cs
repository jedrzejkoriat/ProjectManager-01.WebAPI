using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IUserRoleRepository
{
    Task<List<UserRole>> GetAllAsync();
    Task<bool> CreateAsync(UserRole userRole);
    Task<UserRole> GetByUserIdAsync(Guid userId);
    Task<bool> DeleteAsync(Guid userId);
    Task<bool> UpdateAsync(UserRole userRole);
    Task<bool> DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> CreateAsync(UserRole userRole, IDbTransaction transaction);
}
