using System.Data;
using AutoMapper;
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

    public ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper,
        ITicketService ticketService,
        IDbConnection dbConnection,
        IProjectRoleService projectRoleService)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _ticketService = ticketService;
        _dbConnection = dbConnection;
        _projectRoleService = projectRoleService;
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
            await _projectRepository.DeleteAsync(projectId, transaction);
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
        await _projectRepository.SoftDeleteAsync(projectId);
    }

    public async Task<IEnumerable<ProjectDto>> GetProjectsByUserIdAsync(Guid userId)
    {
        var projects = await _projectRepository.GetByUserIdAsync(userId);

        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
