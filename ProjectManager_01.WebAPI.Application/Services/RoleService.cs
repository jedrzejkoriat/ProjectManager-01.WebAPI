using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Roles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        Role role = mapper.Map<Role>(roleCreateDto);
        await roleRepository.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        Role role = mapper.Map<Role>(roleUpdateDto);
        await roleRepository.UpdateAsync(role);
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        await roleRepository.DeleteAsync(roleId);
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid roleId)
    {
        Role role = await roleRepository.GetByIdAsync(roleId);

        return mapper.Map<RoleDto>(role);
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        List<Role> roles = await roleRepository.GetAllAsync();

        return mapper.Map<List<RoleDto>>(roles);
    }
}
