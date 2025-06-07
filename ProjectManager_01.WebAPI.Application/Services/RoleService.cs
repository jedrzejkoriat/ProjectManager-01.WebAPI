using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Roles;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly IUserRoleService _userRoleService;

    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IUserRoleService userRoleService)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _userRoleService = userRoleService;
    }

    public async Task CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        var role = _mapper.Map<Role>(roleCreateDto);
        role.Id = Guid.NewGuid();
        await _roleRepository.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        var role = _mapper.Map<Role>(roleUpdateDto);
        await _roleRepository.UpdateAsync(role);
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _roleRepository.DeleteByIdAsync(roleId, transaction);
            await _userRoleService.DeleteByRoleIdAsync(roleId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing role deletion transaction.");
        }
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}
