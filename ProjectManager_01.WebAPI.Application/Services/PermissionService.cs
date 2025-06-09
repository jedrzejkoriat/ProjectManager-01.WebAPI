using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly IProjectRolePermissionService _projectRolePermissionService;
    private readonly ILogger<PermissionService> _logger;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService,
        ILogger<PermissionService> logger)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _projectRolePermissionService = projectRolePermissionService;
        _logger = logger;
    }

    public async Task CreatePermissionAsync(PermissionCreateDto permissionCreateDto)
    {
        _logger.LogWarning("Creating Permission called. Permission: {PermissionName}", permissionCreateDto.Name);

        var permission = _mapper.Map<Permission>(permissionCreateDto);
        permission.Id = Guid.NewGuid();

        // Check if operation is successful
        if (!await _permissionRepository.CreateAsync(permission))
        {
            _logger.LogError("Creating Permission failed. Permission: {PermissionName}", permission.Name);
            throw new OperationFailedException("Creating Permission failed.");
        }

        _logger.LogInformation("Creating Permission successful. Permission: {PermissionId}", permission.Id);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        _logger.LogWarning("Deleting Permission transaction called. Permission: {PermissionId}", permissionId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _projectRolePermissionService.DeleteByPermissionIdAsync(permissionId, transaction);

            // Check if operation is successful
            if (!await _permissionRepository.DeleteByIdAsync(permissionId, transaction))
            {
                _logger.LogError("Deleting Permission failed. Permission: {PermissionId}", permissionId);
                throw new OperationFailedException("Deleting Permission transaction failed.");
            }

            transaction.Commit();
            _logger.LogInformation("Deleting Permission transaction successful. Permission: {PermissionId}", permissionId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting Permission transaction failed. Permission: {PermissionId}", permissionId);
            throw;
        }
    }

    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        _logger.LogInformation("Getting Permissions called.");

        var permissions = await _permissionRepository.GetAllAsync();

        _logger.LogInformation("Getting Permissions ({Count}) succesfull.", permissions.Count());
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
    {
        _logger.LogInformation("Getting Permission called. Permission: {PermissionId}", permissionId);

        var permission = await _permissionRepository.GetByIdAsync(permissionId);

        // Check if operation is successful
        if (permission == null)
        {
            _logger.LogError("Getting Permission failed. Permission: {PermissionId}", permissionId);
            throw new NotFoundException("Permission not found.");
        }

        _logger.LogInformation("Getting Permission successful. Permission: {PermissionId}", permissionId);
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto)
    {
        _logger.LogInformation("Updating Permission called. Permission: {PermissionId}", permissionUpdateDto.Id);

        var permission = _mapper.Map<Permission>(permissionUpdateDto);

        // Check if operation is successful
        if (!await _permissionRepository.UpdateAsync(permission))
        {
            _logger.LogError("Updating Permission failed. Permission: {PermissionId}", permission.Id);
            throw new OperationFailedException("Updating Permission failed.");
        }

        _logger.LogInformation("Updating Permission successful. Permission: {PermissionId}", permission.Id);
    }
}
