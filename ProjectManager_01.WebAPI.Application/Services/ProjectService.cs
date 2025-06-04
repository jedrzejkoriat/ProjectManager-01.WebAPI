using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository projectRepository;
    private readonly IMapper mapper;
    private readonly ITicketService ticketService;
    private readonly IDbConnection dbConnection;
    private readonly IProjectRoleService projectRoleService;

    public ProjectService(
        IProjectRepository projectRepository,
        IMapper mapper,
        ITicketService ticketService,
        IDbConnection dbConnection,
        IProjectRoleService projectRoleService)
    {
        this.projectRepository = projectRepository;
        this.mapper = mapper;
        this.ticketService = ticketService;
        this.dbConnection = dbConnection;
        this.projectRoleService = projectRoleService;
    }

    public async Task CreateProjectAsync(ProjectCreateDto projectCreateDto)
    {
        var project = mapper.Map<Project>(projectCreateDto);
        await projectRepository.CreateAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
    {
        var project = mapper.Map<Project>(projectUpdateDto);
        await projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var transaction = dbConnection.BeginTransaction();

        try
        {
            await projectRepository.DeleteAsync(projectId, transaction);
            await ticketService.DeleteByProjectIdAsync(projectId, transaction);
            await projectRoleService.DeleteByProjectIdAsync(projectId, transaction);

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
        var project = await projectRepository.GetByIdAsync(projectId);

        return mapper.Map<ProjectDto>(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await projectRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task SoftDeleteProjectAsync(Guid projectId)
    {
        await projectRepository.SoftDeleteAsync(projectId);
    }
}
