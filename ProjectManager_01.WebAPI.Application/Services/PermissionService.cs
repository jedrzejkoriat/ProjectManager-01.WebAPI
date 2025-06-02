using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IMapper mapper;

    public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
    {
        this.permissionRepository = permissionRepository;
        this.mapper = mapper;
    }

    public async Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto)
    {
        Permission permission = mapper.Map<Permission>(permissionCreateDto);
        await permissionRepository.CreateAsync(permission);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        await permissionRepository.DeleteAsync(permissionId);
    }

    public async Task<List<PermissionDto>> GetAllPermissionsAsync()
    {
        List<Permission> permissions = await permissionRepository.GetAllAsync();

        return mapper.Map<List<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
    {
        Permission permission = await permissionRepository.GetByIdAsync(permissionId);

        return mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto)
    {
        Permission permission = mapper.Map<Permission>(permissionUpdateDto);
        await permissionRepository.UpdateAsync(permission);
    }
}
