using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRolePermissionService : IProjectRolePermissionService
{
    private readonly IProjectRolePermissionRepository _projectRolePermissionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProjectRolePermissionService> _logger;

    public ProjectRolePermissionService(
        IProjectRolePermissionRepository projectRolePermissionRepository,
        IMapper mapper,
        ILogger<ProjectRolePermissionService> logger)
    {
        _projectRolePermissionRepository = projectRolePermissionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto)
    {
        _logger.LogInformation("Creating ProjectRolePermission called. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);

        var projectRolePermission = _mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);

        // Check if operation is successful
        if (!await _projectRolePermissionRepository.CreateAsync(projectRolePermission))
        {
            _logger.LogError("Creating ProjectRolePermission failed. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
                projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);
            throw new OperationFailedException("Creating ProjectRolePermission failed.");
        }

        _logger.LogInformation("Creating ProjectRolePermission successful. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto, IDbTransaction transaction)
    {
        _logger.LogInformation("Creating ProjectRolePermission called. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);

        var projectRolePermission = _mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);
       
        // Check if operation is successful
        if (!await _projectRolePermissionRepository.CreateAsync(projectRolePermission, transaction))
        {
            _logger.LogError("Creating ProjectRolePermission failed. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
                projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);
            throw new OperationFailedException("Creating ProjectRolePermission failed.");
        }

        _logger.LogInformation("Creating ProjectRolePermission successful. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRolePermissionCreateDto.ProjectRoleId, projectRolePermissionCreateDto.PermissionId);
    }

    public async Task DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectRolePermissions by PermissionId called. Permission: {PermissionId}", permissionId);

        // Check if operation is successful
        if (!await _projectRolePermissionRepository.DeleteAllByPermissionIdAsync(permissionId, transaction))
        {
            _logger.LogError("Deleting ProjectRolePermissions by PermissionId ailed. Permission: {PermissionId}", permissionId);
            throw new OperationFailedException("Deleting ProjectRolePermissions failed.");
        }

        _logger.LogInformation("Deleting ProjectRolePermissions by PermissionId successful. Permission: {PermissionId}", permissionId);
    }

    public async Task DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectRolePermissions by ProjectRoleId called. ProjectRole: {ProjectRoleId}", projectRoleId);

        // Check if operation is successful
        if (!await _projectRolePermissionRepository.DeleteAllByProjectRoleIdAsync(projectRoleId, transaction))
        {
            _logger.LogError("Deleting ProjectRolePermissions by ProjectRoleId failed. ProjectRole: {ProjectRoleId}", projectRoleId);
            throw new OperationFailedException("Deleting ProjectRolePermissions failed.");
        }

        _logger.LogInformation("Deleting ProjectRolePermissions by ProjectRoleId successful. ProjectRole: {ProjectRoleId}", projectRoleId);
    }

    public async Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId)
    {
        _logger.LogInformation("Deleting ProjectRolePermission called. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRoleId, permissionId);

        // Check if operation is successful
        if (!await _projectRolePermissionRepository.DeleteByProjectRoleIdAndPermissionIdAsync(projectRoleId, permissionId))
        {
            _logger.LogError("Deleting ProjectRolePermission failed. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
                projectRoleId, permissionId);
            throw new OperationFailedException("Deleting ProjectRolePermission failed.");
        }

        _logger.LogInformation("Deleting ProjectRolePermission successful. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRoleId, permissionId);
    }

    public async Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId)
    {
        _logger.LogInformation("Getting ProjectRolePermission called. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRoleId, permissionId);

        var projectRolePermission = await _projectRolePermissionRepository.GetByIdAsync(projectRoleId, permissionId);

        // Check if operation is successful
        if (projectRolePermission == null)
        {
            _logger.LogError("ProjectRolePermission not found. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
                projectRoleId, permissionId);
            throw new NotFoundException("ProjectRolePermission not found.");
        }

        _logger.LogInformation("Getting ProjectRolePermission successful. ProjectRole: {ProjectRoleId}, Permission: {PermissionId}",
            projectRoleId, permissionId);
        return _mapper.Map<ProjectRolePermissionDto>(projectRolePermission);
    }

    public async Task<IEnumerable<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync()
    {
        _logger.LogInformation("Getting all ProjectRolePermissions called.");

        var projectRolePermissions = await _projectRolePermissionRepository.GetAllAsync();

        _logger.LogInformation("Getting ProjectRolePermissions ({Count}) successful.", projectRolePermissions.Count());
        return _mapper.Map<IEnumerable<ProjectRolePermissionDto>>(projectRolePermissions);
    }
}
