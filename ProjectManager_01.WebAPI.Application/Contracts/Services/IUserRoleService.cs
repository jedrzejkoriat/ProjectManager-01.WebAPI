using ProjectManager_01.Application.DTOs.UserRoles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IUserRoleService
{
    Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto);
    Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto);
    Task DeleteUserRoleAsync(Guid userRoleId);
    Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userRoleId);
    Task<List<UserRoleDto>> GetAllUserRolesAsync();
}
