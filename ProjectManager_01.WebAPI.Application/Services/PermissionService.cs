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
        _logger.LogWarning("Creating permission called. Permission: {PermissionName}", permissionCreateDto.Name);

        var permission = _mapper.Map<Permission>(permissionCreateDto);
        permission.Id = Guid.NewGuid();

        // Check if operation is successful
        if (!await _permissionRepository.CreateAsync(permission))
        {
            _logger.LogError("Creating permission failed. Permission: {PermissionName}", permission.Name);
            throw new Exception("Creating permission failed.");
        }

        _logger.LogInformation("Creating permission successful. Permission: {PermissionId}", permission.Id);
    }

    public async Task DeletePermissionAsync(Guid permissionId)
    {
        _logger.LogWarning("Deleting permission transaction called. Permission: {PermissionId}", permissionId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            // Check if operation is successful
            if (!await _permissionRepository.DeleteByIdAsync(permissionId, transaction))
            {
                _logger.LogError("Deleting permission transaction failed. Permission: {PermissionId}", permissionId);
                throw new OperationFailedException("Deleting permission transaction failed.");
            }

            await _projectRolePermissionService.DeleteByPermissionIdAsync(permissionId, transaction);

            transaction.Commit();
            _logger.LogInformation("Deleting permission transaction successful. Permission: {PermissionId}", permissionId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting permission transaction failed. Permission: {PermissionId}", permissionId);
            throw;
        }
    }

    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        _logger.LogInformation("Getting permissions called.");

        var permissions = await _permissionRepository.GetAllAsync();
        _logger.LogInformation("Getting permissions ({Count}) succesfull.", permissions.Count());

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(Guid permissionId)
    {
        _logger.LogInformation("Getting permission called. Permission: {PermissionId}", permissionId);

        var permission = await _permissionRepository.GetByIdAsync(permissionId);

        // Check if operation is successful
        if (permission == null)
        {
            _logger.LogError("Getting permission failed. Permission: {PermissionId}", permissionId);
            throw new NotFoundException("Permission not found.");
        }

        _logger.LogInformation("Getting permission successful. Permission: {PermissionId}", permissionId);
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermissionAsync(PermissionUpdateDto permissionUpdateDto)
    {
        _logger.LogInformation("Updating permission called. Permission: {PermissionId}", permissionUpdateDto.Id);

        var permission = _mapper.Map<Permission>(permissionUpdateDto);

        // Check if operation is successful
        if (!await _permissionRepository.UpdateAsync(permission))
        {
            _logger.LogError("Updating permission failed. Permission: {PermissionId}", permission.Id);
            throw new OperationFailedException("Updating permission failed.");
        }

        _logger.LogInformation("Updating permission successful. Permission: {PermissionId}", permission.Id);
    }
}
