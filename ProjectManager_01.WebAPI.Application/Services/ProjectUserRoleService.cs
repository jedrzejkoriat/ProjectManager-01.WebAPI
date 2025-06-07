using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectUserRoleService : IProjectUserRoleService
{
    private readonly IProjectUserRoleRepository _projectUserRoleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProjectUserRoleService> _logger;

    public ProjectUserRoleService(
        IProjectUserRoleRepository projectUserRoleRepository,
        IMapper mapper,
        ILogger<ProjectUserRoleService> logger)
    {
        _projectUserRoleRepository = projectUserRoleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto)
    {
        _logger.LogInformation("Creating ProjectUserRole called. Project: {ProjectId}, User: {UserId}", projectUserRoleCreateDto.ProjectId, projectUserRoleCreateDto.UserId);

        var projectUserRole = _mapper.Map<ProjectUserRole>(projectUserRoleCreateDto);
        projectUserRole.Id = Guid.NewGuid();

        // Check if operation is successful
        if (!await _projectUserRoleRepository.CreateAsync(projectUserRole))
        {
            _logger.LogError("Creating ProjectUserRole failed. Project: {ProjectId}, User: {UserId}", projectUserRoleCreateDto.ProjectId, projectUserRoleCreateDto.UserId);
            throw new OperationFailedException("Create ProjectUserRole failed.");
        }

        _logger.LogInformation("Creating ProjectUserRole successful. Id: {ProjectUserRoleId}", projectUserRole.Id);
    }

    public async Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto)
    {
        _logger.LogInformation("Updating ProjectUserRole called. Id: {ProjectUserRoleId}", projectUserRoleUpdateDto.Id);

        var projectUserRole = _mapper.Map<ProjectUserRole>(projectUserRoleUpdateDto);

        // Check if operation is successful
        if (!await _projectUserRoleRepository.UpdateAsync(projectUserRole))
        {
            _logger.LogError("Updating ProjectUserRole failed. Id: {ProjectUserRoleId}", projectUserRole.Id);
            throw new OperationFailedException("Update ProjectUserRole failed.");
        }

        _logger.LogInformation("Updating ProjectUserRole successful. Id: {ProjectUserRoleId}", projectUserRole.Id);
    }

    public async Task DeleteProjectUserRoleAsync(Guid projectUserRoleId)
    {
        _logger.LogInformation("Deleting ProjectUserRole called. Id: {ProjectUserRoleId}", projectUserRoleId);

        // Check if operation is successful
        if (!await _projectUserRoleRepository.DeleteByIdAsync(projectUserRoleId))
        {
            _logger.LogError("Deleting ProjectUserRole failed. Id: {ProjectUserRoleId}", projectUserRoleId);
            throw new OperationFailedException("Delete ProjectUserRole failed.");
        }

        _logger.LogInformation("Deleting ProjectUserRole successful. Id: {ProjectUserRoleId}", projectUserRoleId);
    }

    public async Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId)
    {
        _logger.LogInformation("Getting ProjectUserRole called. Id: {ProjectUserRoleId}", projectUserRoleId);

        var projectUserRole = await _projectUserRoleRepository.GetByIdAsync(projectUserRoleId);

        // Check if operation is successful
        if (projectUserRole == null)
        {
            _logger.LogError("Getting ProjectUserRole failed. Id: {ProjectUserRoleId}", projectUserRoleId);
            throw new NotFoundException("ProjectUserRole not found.");
        }

        _logger.LogInformation("Getting ProjectUserRole successful. Id: {ProjectUserRoleId}", projectUserRoleId);
        return _mapper.Map<ProjectUserRoleDto>(projectUserRole);
    }

    public async Task<IEnumerable<ProjectUserRoleDto>> GetAllProjectUserRolesAsync()
    {
        _logger.LogInformation("Getting all ProjectUserRoles called.");

        var projectUserRoles = await _projectUserRoleRepository.GetAllAsync();

        _logger.LogInformation("Getting all ProjectUserRoles successful. Count: {Count}", projectUserRoles.Count());
        return _mapper.Map<IEnumerable<ProjectUserRoleDto>>(projectUserRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectUserRoles by ProjectId called. ProjectId: {ProjectId}", projectId);

        if (!await _projectUserRoleRepository.DeleteAllByProjectIdAsync(projectId, transaction))
        {
            _logger.LogError("Deleting ProjectUserRoles by ProjectId failed. ProjectId: {ProjectId}", projectId);
            throw new OperationFailedException("Delete ProjectUserRoles failed.");
        }

        _logger.LogInformation("Deleting ProjectUserRoles by ProjectId successful. ProjectId: {ProjectId}", projectId);
    }

    public async Task DeleteByProjectRoleId(Guid projectRoleId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectUserRoles by ProjectRoleId called. ProjectRoleId: {ProjectRoleId}", projectRoleId);

        // Check if operation is successful
        if (!await _projectUserRoleRepository.DeleteAllByProjectRoleIdAsync(projectRoleId, transaction))
        {
            _logger.LogError("Deleting ProjectUserRoles by ProjectRoleId failed. ProjectRoleId: {ProjectRoleId}", projectRoleId);
            throw new OperationFailedException("Delete ProjectUserRoles by ProjectRoleId failed.");
        }

        _logger.LogInformation("Deleting ProjectUserRoles by ProjectRoleId successful. ProjectRoleId: {ProjectRoleId}", projectRoleId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting ProjectUserRoles by UserId called. UserId: {UserId}", userId);

        // Check if operation is successful
        if (!await _projectUserRoleRepository.DeleteByUserIdAsync(userId, transaction))
        {
            _logger.LogError("Deleting ProjectUserRoles by UserId failed. UserId: {UserId}", userId);
            throw new OperationFailedException("Delete ProjectUserRoles by UserId failed.");
        }

        _logger.LogInformation("Deleting ProjectUserRoles by UserId successful. UserId: {UserId}", userId);
    }

    public async Task<IEnumerable<ProjectUserRoleDto>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId)
    {
        _logger.LogInformation("Getting ProjectUserRoles by UserId and ProjectId called. UserId: {UserId}, ProjectId: {ProjectId}", userId, projectId);

        var projectUserRole = await _projectUserRoleRepository.GetAllByUserIdAndProjectIdAsync(userId, projectId);

        // Check if operation is successful
        if (projectUserRole == null || !projectUserRole.Any())
        {
            _logger.LogError("Getting ProjectUserRoles by UserId and ProjectId failed. UserId: {UserId}, ProjectId: {ProjectId}", userId, projectId);
            throw new NotFoundException("ProjectUserRoles not found.");
        }

        _logger.LogInformation("Getting ProjectUserRoles by UserId and ProjectId successful. Count: {Count}", projectUserRole.Count());
        return _mapper.Map<List<ProjectUserRoleDto>>(projectUserRole);
    }
}
