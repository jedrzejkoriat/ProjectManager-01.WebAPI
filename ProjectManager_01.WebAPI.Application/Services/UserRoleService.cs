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

    public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
    {
        this.userRoleRepository = userRoleRepository;
        this.mapper = mapper;
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
    {
        UserRole userRole = mapper.Map<UserRole>(userRoleCreateDto);
        await userRoleRepository.CreateAsync(userRole);
    }

    public async Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto)
    {
        UserRole userRole = mapper.Map<UserRole>(userRoleUpdateDto);
        await userRoleRepository.UpdateAsync(userRole);
    }

    public async Task DeleteUserRoleAsync(Guid userId)
    {
        await userRoleRepository.DeleteAsync(userId);
    }

    public async Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userId)
    {
        UserRole userRole = await userRoleRepository.GetByUserIdAsync(userId);

        return mapper.Map<UserRoleDto>(userRole);
    }

    public async Task<List<UserRoleDto>> GetAllUserRolesAsync()
    {
        List<UserRole> userRoles = await userRoleRepository.GetAllAsync();

        return mapper.Map<List<UserRoleDto>>(userRoles);
    }

    public async Task DeleteByRoleIdAsync(Guid roleId, IDbConnection connection, IDbTransaction transaction)
    {
        await userRoleRepository.DeleteByRoleIdAsync(roleId, connection, transaction);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction)
    {
        await userRoleRepository.DeleteByUserIdAsync(userId, connection, transaction);
    }
}
