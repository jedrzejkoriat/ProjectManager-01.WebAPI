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
    Task<List<UserRole>> GetByRoleIdAsync(Guid roleId);
    Task<bool> DeleteByRoleIdAsync(Guid roleId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
}
