using System.Data;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly ITicketService _ticketService;
    private readonly IDbConnection _dbConnection;
    private readonly IProjectRoleService _projectRoleService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper,
        ITicketService ticketService,
        IDbConnection dbConnection,
        IProjectRoleService projectRoleService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _ticketService = ticketService;
        _dbConnection = dbConnection;
        _projectRoleService = projectRoleService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task CreateProjectAsync(ProjectCreateDto projectCreateDto)
    {
        _logger.LogInformation("Creating Project called. Project: {ProjectName}", projectCreateDto.Name);

        var project = _mapper.Map<Project>(projectCreateDto);
        project.Id = Guid.NewGuid();
        project.CreatedAt = DateTimeOffset.UtcNow;
        project.Key.ToUpper();

        // Check if operation is successful
        if (!await _projectRepository.CreateAsync(project))
        {
            _logger.LogError("Creating Project failed. Project: {ProjectName}", projectCreateDto.Name);
            throw new OperationFailedException("Creating project failed.");
        }

        _logger.LogInformation("Creating Project successful. Project: {ProjectId}", project.Id);
    }

    public async Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
    {
        _logger.LogInformation("Updating Project called. Project: {ProjectId}", projectUpdateDto.Id);

        var project = _mapper.Map<Project>(projectUpdateDto);
        project.Key.ToUpper();

        // Check if operation is successful
        if (!await _projectRepository.UpdateAsync(project))
        {
            _logger.LogError("Updating Project failed. Project: {ProjectId}", projectUpdateDto.Id);
            throw new OperationFailedException("Updating project failed.");
        }

        _logger.LogInformation("Updating Project successful. Project: {ProjectId}", projectUpdateDto.Id);
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        _logger.LogWarning("Deleting Project transaction called. Project: {ProjectId}", projectId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            // Check if operation is successful
            if (!await _projectRepository.DeleteByIdAsync(projectId, transaction))
            {
                _logger.LogError("Deleting Project failed. Project: {ProjectId}", projectId);
                throw new OperationFailedException("Deleting Project transaction failed.");
            }

            await _ticketService.DeleteByProjectIdAsync(projectId, transaction);
            await _projectRoleService.DeleteByProjectIdAsync(projectId, transaction);

            transaction.Commit();
            _logger.LogInformation("Deleting Project transaction successful. Project: {ProjectId}", projectId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting Project transaction failed. Project: {ProjectId}", projectId);
            throw;
        }
    }

    public async Task<ProjectDto> GetProjectByIdAsync(Guid projectId)
    {
        _logger.LogInformation("Getting Project called. Project: {ProjectId}", projectId);

        var project = await _projectRepository.GetByIdAsync(projectId);

        // Check if operation is successful
        if (project == null)
        {
            _logger.LogError("Getting Project failed. Project: {ProjectId}", projectId);
            throw new NotFoundException("Project not found.");
        }

        _logger.LogInformation("Getting Project successful. Project: {ProjectId}", projectId);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        _logger.LogInformation("Getting all Projects called.");

        var projects = await _projectRepository.GetAllAsync();

        _logger.LogInformation("Getting all Projects ({Count}) successful.", projects.Count());
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task SoftDeleteProjectAsync(Guid projectId)
    {
        _logger.LogInformation("Soft deleting Project called. Project: {ProjectId}", projectId);

        // Check if operation is successful
        if (!await _projectRepository.SoftDeleteByIdAsync(projectId))
        {
            _logger.LogError("Soft deleting Project failed. Project: {ProjectId}", projectId);
            throw new OperationFailedException("Soft deleting project failed.");
        }

        _logger.LogInformation("Soft deleting Project successful. Project: {ProjectId}", projectId);
    }

    public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync()
    {
        _logger.LogInformation("Getting User Projects called.");

        // Extract project IDs from user claims
        var projectIds = _httpContextAccessor.HttpContext?.User.Claims
            .Where(c => c.Type == "ProjectPermission")
            .Select(c => c.Value.Split(':'))
            .Where(parts => parts.Length == 2 && parts[1].Equals(Permissions.ReadProject, StringComparison.OrdinalIgnoreCase))
            .Select(parts => parts[0])
            .Distinct()
            .ToList();

        var projects = await Task.WhenAll(projectIds.Select(id => _projectRepository.GetByIdAsync(Guid.Parse(id))));
        var projectsList = projects.ToList();

        _logger.LogInformation("Getting User Projects successful. Count: {Count}", projectsList.Count);
        return _mapper.Map<IEnumerable<ProjectDto>>(projectsList);
    }
}
