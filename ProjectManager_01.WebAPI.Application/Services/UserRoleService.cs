using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IMapper mapper;

    public UserRoleService(
        IUserRoleRepository userRoleRepository,
        IMapper mapper)
    {
        this.userRoleRepository = userRoleRepository;
        this.mapper = mapper;
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
    {
        var userRole = mapper.Map<UserRole>(userRoleCreateDto);
        await userRoleRepository.CreateAsync(userRole);
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto, IDbTransaction transaction)
    {
        var userRole = mapper.Map<UserRole>(userRoleCreateDto);
        await userRoleRepository.CreateAsync(userRole, transaction);
    }

    public async Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto)
    {
        var userRole = mapper.Map<UserRole>(userRoleUpdateDto);
        await userRoleRepository.UpdateAsync(userRole);
    }

    public async Task DeleteUserRoleAsync(Guid userId)
    {
        await userRoleRepository.DeleteAsync(userId);
    }

    public async Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userId)
    {
        var userRole = await userRoleRepository.GetByUserIdAsync(userId);

        return mapper.Map<UserRoleDto>(userRole);
    }

    public async Task<IEnumerable<UserRoleDto>> GetAllUserRolesAsync()
    {
        var userRoles = await userRoleRepository.GetAllAsync();

        return mapper.Map<IEnumerable<UserRoleDto>>(userRoles);
    }

    public async Task DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction)
    {
        await userRoleRepository.DeleteByRoleIdAsync(roleId, transaction);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await userRoleRepository.DeleteByUserIdAsync(userId, transaction);
    }
}
