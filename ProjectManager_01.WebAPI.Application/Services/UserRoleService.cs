using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IMapper _mapper;

    public UserRoleService(
        IUserRoleRepository userRoleRepository,
        IMapper mapper)
    {
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
    {
        var userRole = _mapper.Map<UserRole>(userRoleCreateDto);
        await _userRoleRepository.CreateAsync(userRole);
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto, IDbTransaction transaction)
    {
        var userRole = _mapper.Map<UserRole>(userRoleCreateDto);
        await _userRoleRepository.CreateAsync(userRole, transaction);
    }

    public async Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto)
    {
        var userRole = _mapper.Map<UserRole>(userRoleUpdateDto);
        await _userRoleRepository.UpdateAsync(userRole);
    }

    public async Task DeleteUserRoleAsync(Guid userId)
    {
        await _userRoleRepository.DeleteAsync(userId);
    }

    public async Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userId)
    {
        var userRole = await _userRoleRepository.GetByUserIdAsync(userId);

        return _mapper.Map<UserRoleDto>(userRole);
    }

    public async Task<IEnumerable<UserRoleDto>> GetAllUserRolesAsync()
    {
        var userRoles = await _userRoleRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);
    }

    public async Task DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction)
    {
        await _userRoleRepository.DeleteByRoleIdAsync(roleId, transaction);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await _userRoleRepository.DeleteByUserIdAsync(userId, transaction);
    }
}
