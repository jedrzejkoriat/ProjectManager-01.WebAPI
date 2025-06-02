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

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        this.projectRepository = projectRepository;
        this.mapper = mapper;
    }

    public async Task CreateProjectAsync(ProjectCreateDto projectCreateDto)
    {
        Project project = mapper.Map<Project>(projectCreateDto);
        await projectRepository.CreateAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
    {
        Project project = mapper.Map<Project>(projectUpdateDto);
        await projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        await projectRepository.DeleteAsync(projectId);
    }

    public async Task<ProjectDto> GetProjectByIdAsync(Guid projectId)
    {
        Project project = await projectRepository.GetByIdAsync(projectId);

        return mapper.Map<ProjectDto>(project);
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        List<Project> projects = await projectRepository.GetAllAsync();

        return mapper.Map<List<ProjectDto>>(projects);
    }
}
