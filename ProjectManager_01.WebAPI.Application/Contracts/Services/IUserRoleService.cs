using System.Data;
using System.Transactions;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.Services;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IUserRoleService
{
    Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto);
    Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto);
    Task DeleteUserRoleAsync(Guid userRoleId);
    Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userRoleId);
    Task<List<UserRoleDto>> GetAllUserRolesAsync();
    Task DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto, IDbTransaction transaction);
}
