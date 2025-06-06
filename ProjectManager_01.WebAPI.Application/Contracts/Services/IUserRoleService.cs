using System.Data;
using ProjectManager_01.Application.DTOs.UserRoles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IUserRoleService
{
    Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto);
    Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto);
    Task DeleteUserRoleAsync(Guid userRoleId);
    Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userRoleId);
    Task<IEnumerable<UserRoleDto>> GetAllUserRolesAsync();
    Task DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto, IDbTransaction transaction);
}
