using ProjectManager_01.Application.DTOs.Roles;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IRoleService
{
    Task CreateRoleAsync(RoleCreateDto roleCreateDto);
    Task UpdateRoleAsync(RoleUpdateDto roleUpdateDto);
    Task DeleteRoleAsync(Guid roleId);
    Task<RoleDto> GetRoleByIdAsync(Guid roleId);
    Task<List<RoleDto>> GetAllRolesAsync();
}
