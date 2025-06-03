using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IMapper mapper;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public PermissionService(IPermissionRepository permissionRepository, 
        IMapper mapper, 
        IDbConnectionFactory dbConnectionFactory, 
        IProjectRolePermissionService projectRolePermissionService)
    {
        this.permissionRepository = permissionRepository;
        this.mapper = mapper;
        this.dbConnectionFactory = dbConnectionFactory;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto)
    {
        Permission permission = mapper.Map<Permission>(permissionCreateDto);
        await permissionRepository.CreateAsync(permission);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        if (connection is SqlConnection sqlConnection)
            await sqlConnection.OpenAsync();
        else
            connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            await permissionRepository.DeleteAsync(permissionId, connection, transaction);
            await projectRolePermissionService.DeleteByPermissionIdAsync(permissionId, connection, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing permission deletion transaction.");
        }
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
