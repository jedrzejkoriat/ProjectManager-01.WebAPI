using System.Data;
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
    private readonly IDbConnection dbConnection;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService)
    {
        this.permissionRepository = permissionRepository;
        this.mapper = mapper;
        this.dbConnection = dbConnection;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto)
    {
        var permission = mapper.Map<Permission>(permissionCreateDto);
        await permissionRepository.CreateAsync(permission);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var transaction = dbConnection.BeginTransaction();

        try
        {
            await permissionRepository.DeleteAsync(permissionId, transaction);
            await projectRolePermissionService.DeleteByPermissionIdAsync(permissionId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing permission deletion transaction.");
        }
    }

    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        var permissions = await permissionRepository.GetAllAsync();

        return mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
    {
        var permission = await permissionRepository.GetByIdAsync(permissionId);

        return mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto)
    {
        var permission = mapper.Map<Permission>(permissionUpdateDto);
        await permissionRepository.UpdateAsync(permission);
    }
}
