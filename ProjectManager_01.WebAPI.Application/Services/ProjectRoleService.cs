using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IMapper mapper;

    public ProjectRoleService(IProjectRoleRepository projectRoleRepository, IMapper mapper)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.mapper = mapper;
    }

    public async Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto)
    {
        ProjectRole projectRole = mapper.Map<ProjectRole>(projectRoleCreateDto);
        await projectRoleRepository.CreateAsync(projectRole);
    }

    public async Task UpdateProjectRoleAsync(ProjectRoleUpdateDto projectRoleUpdateDto)
    {
        ProjectRole projectRole = mapper.Map<ProjectRole>(projectRoleUpdateDto);
        await projectRoleRepository.UpdateAsync(projectRole);
    }

    public async Task DeleteProjectRoleAsync(Guid projectRoleId)
    {
        await projectRoleRepository.DeleteAsync(projectRoleId);
    }

    public async Task<ProjectRoleDto> GetProjectRoleByIdAsync(Guid projectRoleId)
    {
        ProjectRole projectRole = await projectRoleRepository.GetByIdAsync(projectRoleId);

        return mapper.Map<ProjectRoleDto>(projectRole);
    }

    public async Task<List<ProjectRoleDto>> GetAllProjectRolesAsync()
    {
        List<ProjectRole> projectRoles = await projectRoleRepository.GetAllAsync();

        return mapper.Map<List<ProjectRoleDto>>(projectRoles);
    }
}

