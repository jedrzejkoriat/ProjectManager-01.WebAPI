using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly IProjectRolePermissionService _projectRolePermissionService;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _projectRolePermissionService = projectRolePermissionService;
    }

    public async Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto)
    {
        var permission = _mapper.Map<Permission>(permissionCreateDto);
        await _permissionRepository.CreateAsync(permission);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _permissionRepository.DeleteAsync(permissionId, transaction);
            await _projectRolePermissionService.DeleteByPermissionIdAsync(permissionId, transaction);

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
        var permissions = await _permissionRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
    {
        var permission = await _permissionRepository.GetByIdAsync(permissionId);

        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto)
    {
        var permission = _mapper.Map<Permission>(permissionUpdateDto);
        await _permissionRepository.UpdateAsync(permission);
    }
}
