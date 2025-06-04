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
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;
    private readonly IDbConnection dbConnection;
    private readonly IUserRoleService userRoleService;

    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IUserRoleService userRoleService)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
        this.dbConnection = dbConnection;
        this.userRoleService = userRoleService;
    }

    public async Task CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        var role = mapper.Map<Role>(roleCreateDto);
        await roleRepository.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        var role = mapper.Map<Role>(roleUpdateDto);
        await roleRepository.UpdateAsync(role);
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(dbConnection);

        try
        {
            await roleRepository.DeleteAsync(roleId, transaction);
            await userRoleService.DeleteByRoleIdAsync(roleId, transaction);

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
        var role = await roleRepository.GetByIdAsync(roleId);

        return mapper.Map<RoleDto>(role);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await roleRepository.GetAllAsync();

        return mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}
