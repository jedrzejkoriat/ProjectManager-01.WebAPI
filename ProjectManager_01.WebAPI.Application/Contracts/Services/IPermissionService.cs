using ProjectManager_01.Application.DTOs.Permissions;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IPermissionService
{
    Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto);
    Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto);
    Task DeletePermissionAsync(Guid permissionId);
    Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId);
    Task<List<PermissionDto>> GetAllPermissionsAsync();
}
