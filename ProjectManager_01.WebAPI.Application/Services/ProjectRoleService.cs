using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository _projectRoleRepository;
    private readonly IMapper _mapper;
    private readonly IProjectUserRoleService _projectUserRoleService;
    private readonly IDbConnection _dbConnection;
    private readonly IProjectRolePermissionService _projectRolePermissionService;
    private readonly ILogger<ProjectRoleService> _logger;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IMapper mapper,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService,
        ILogger<ProjectRoleService> logger)
    {
        _projectRoleRepository = projectRoleRepository;
        _mapper = mapper;
        _projectUserRoleService = projectUserRoleService;
        _dbConnection = dbConnection;
        _projectRolePermissionService = projectRolePermissionService;
        _logger = logger;
    }

    public async Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto)
    {
        _logger.LogInformation("Creating ProjectRole transaction called. ProjectRoleName: {ProjectRoleName}", projectRoleCreateDto.Name);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var projectRole = _mapper.Map<ProjectRole>(projectRoleCreateDto);
            projectRole.Id = Guid.NewGuid();
            await _projectRoleRepository.CreateAsync(projectRole, transaction);

            foreach (var permissionId in projectRoleCreateDto.PermissionIds)
            {
                var projectRolePermissionDto = new ProjectRolePermissionCreateDto(projectRole.Id, permissionId);
                await _projectRolePermissionService.CreateProjectRolePermissionAsync(projectRolePermissionDto, transaction);
            }

            transaction.Commit();
            _logger.LogInformation("Creating ProjectRole transaction successful. ProjectRole: {ProjectRoleId}", projectRole.Id);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Creating ProjectRole transaction failed. ProjectRole: {ProjectRoleName}", projectRoleCreateDto.Name);
            throw new OperationFailedException("Creating ProjectRole failed.");
        }
    }

    public async Task UpdateProjectRoleAsync(ProjectRoleUpdateDto projectRoleUpdateDto)
    {
        _logger.LogInformation("Updating ProjectRole called. ProjectRole: {ProjectRoleId}", projectRoleUpdateDto.Id);

        var projectRole = _mapper.Map<ProjectRole>(projectRoleUpdateDto);

        // Check if operation is successful
        if (!await _projectRoleRepository.UpdateAsync(projectRole))
        {
            _logger.LogError("Updating ProjectRole failed. ProjectRole: {ProjectRoleId}", projectRoleUpdateDto.Id);
            throw new OperationFailedException("Updating ProjectRole failed.");
        }

        _logger.LogInformation("Updating ProjectRole successful. ProjectRole: {ProjectRoleId}", projectRoleUpdateDto.Id);
    }

    public async Task DeleteProjectRoleAsync(Guid projectRoleId)
    {
        _logger.LogInformation("Deleting ProjectRole called. ProjectRole: {ProjectRoleId}", projectRoleId);
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _projectUserRoleService.DeleteByProjectRoleId(projectRoleId, transaction);
            await _projectRolePermissionService.DeleteByProjectRoleIdAsync(projectRoleId, transaction);

            // Check if operation is successful
            if (!await _projectRoleRepository.DeleteByIdAsync(projectRoleId, transaction))
            {
                _logger.LogError("Deleting ProjectRole failed. ProjectRole: {ProjectRoleId}", projectRoleId);
                throw new OperationFailedException("Deleting ProjectRole failed.");
            }

            transaction.Commit();
            _logger.LogInformation("Deleting ProjectRole successful. ProjectRole: {ProjectRoleId}", projectRoleId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting ProjectRole failed. ProjectRole: {ProjectRoleId}", projectRoleId);
            throw new OperationFailedException("Deleting ProjectRole failed.");
        }
    }

    public async Task<ProjectRoleDto> GetProjectRoleByIdAsync(Guid projectRoleId)
    {
        _logger.LogInformation("Getting ProjectRole called. ProjectRole: {ProjectRoleId}", projectRoleId);

        var projectRole = await _projectRoleRepository.GetByIdAsync(projectRoleId);

        // Check if operation is successful
        if (projectRole == null)
        {
            _logger.LogError("Getting ProjectRole failed. ProjectRole: {ProjectRoleId}", projectRoleId);
            throw new NotFoundException("ProjectRole not found.");
        }

        _logger.LogInformation("Getting ProjectRole successful. ProjectRole: {ProjectRoleId}", projectRoleId);
        return _mapper.Map<ProjectRoleDto>(projectRole);
    }

    public async Task<IEnumerable<ProjectRoleDto>> GetAllProjectRolesAsync()
    {
        _logger.LogInformation("Getting ProjectRoles called.");

        var projectRoles = await _projectRoleRepository.GetAllAsync();

        _logger.LogInformation("Getting ProjectRoles ({Count}) successful.", projectRoles.Count());
        return _mapper.Map<IEnumerable<ProjectRoleDto>>(projectRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectRoles by projectId called. Project: {ProjectId}", projectId);

        // Check if operation is successful
        if (!await _projectRoleRepository.DeleteAllByProjectIdAsync(projectId, transaction))
        {
            _logger.LogWarning("No ProjectRoles related to Project: {ProjectId}", projectId);
            return;
        }

        await _projectUserRoleService.DeleteByProjectIdAsync(projectId, transaction);

        _logger.LogInformation("Deleting ProjectRoles by projectId successful. Project: {ProjectId}", projectId);
    }
}
