using System.Data;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;
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

    public ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper,
        ITicketService ticketService,
        IDbConnection dbConnection,
        IProjectRoleService projectRoleService,
        IHttpContextAccessor httpContextAccessor)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _ticketService = ticketService;
        _dbConnection = dbConnection;
        _projectRoleService = projectRoleService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateProjectAsync(ProjectCreateDto projectCreateDto)
    {
        var project = _mapper.Map<Project>(projectCreateDto);
        await _projectRepository.CreateAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
    {
        var project = _mapper.Map<Project>(projectUpdateDto);
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _projectRepository.DeleteByIdAsync(projectId, transaction);
            await _ticketService.DeleteByProjectIdAsync(projectId, transaction);
            await _projectRoleService.DeleteByProjectIdAsync(projectId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing project deletion transaction.");
        }
    }

    public async Task<ProjectDto> GetProjectByIdAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task SoftDeleteProjectAsync(Guid projectId)
    {
        await _projectRepository.SoftDeleteByIdAsync(projectId);
    }

    public async Task<IEnumerable<ProjectDto>> GetUserProjectsAsync()
    {
        var projectIds = _httpContextAccessor.HttpContext?.User.Claims
            .Where(c => c.Type == "ProjectPermission")
            .Select(c => c.Value.Split(':'))
            .Where(parts => parts.Length == 2 && parts[1].Equals(Permissions.ReadProject, StringComparison.OrdinalIgnoreCase))
            .Select(parts => parts[0])
            .Distinct()
            .ToList();

        var projects = await Task.WhenAll(projectIds.Select(id => _projectRepository.GetByIdAsync(Guid.Parse(id))));
        var projectsList = projects.ToList();

        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
