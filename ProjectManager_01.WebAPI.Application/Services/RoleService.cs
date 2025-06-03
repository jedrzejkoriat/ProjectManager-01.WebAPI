using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Roles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly IUserRoleService userRoleService;

    public RoleService(IRoleRepository roleRepository, IMapper mapper, IDbConnectionFactory dbConnectionFactory, IUserRoleService userRoleService)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
        this.dbConnectionFactory = dbConnectionFactory;
        this.userRoleService = userRoleService;
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
        using var connection = dbConnectionFactory.CreateConnection();

        if (connection is SqlConnection sqlConnection)
            await sqlConnection.OpenAsync();
        else
            connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        { 
            await roleRepository.DeleteAsync(roleId);
            await userRoleService.DeleteByRoleIdAsync(roleId, connection, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception ("Error while performing role deletion transaction.");
        }
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
