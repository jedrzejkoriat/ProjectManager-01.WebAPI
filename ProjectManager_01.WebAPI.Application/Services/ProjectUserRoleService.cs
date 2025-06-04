using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectUserRoleService : IProjectUserRoleService
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IMapper mapper;

    public ProjectUserRoleService(
        IProjectUserRoleRepository projectUserRoleRepository,
        IMapper mapper)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.mapper = mapper;
    }

    public async Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto)
    {
        var projectUserRole = mapper.Map<ProjectUserRole>(projectUserRoleCreateDto);
        await projectUserRoleRepository.CreateAsync(projectUserRole);
    }

    public async Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto)
    {
        var projectUserRole = mapper.Map<ProjectUserRole>(projectUserRoleUpdateDto);
        await projectUserRoleRepository.UpdateAsync(projectUserRole);
    }

    public async Task DeleteProjectUserRoleAsync(Guid projectUserRoleId)
    {
        await projectUserRoleRepository.DeleteAsync(projectUserRoleId);
    }

    public async Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId)
    {
        var projectUserRole = await projectUserRoleRepository.GetByIdAsync(projectUserRoleId);

        return mapper.Map<ProjectUserRoleDto>(projectUserRole);
    }

    public async Task<IEnumerable<ProjectUserRoleDto>> GetAllProjectUserRolesAsync()
    {
        var projectUserRoles = await projectUserRoleRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProjectUserRoleDto>>(projectUserRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        await projectUserRoleRepository.DeleteByProjectIdAsync(projectId, transaction);
    }

    public async Task DeleteByProjectRoleId(Guid projectRoleId, IDbTransaction transaction)
    {
        await projectUserRoleRepository.DeleteByProjectRoleIdAsync(projectRoleId, transaction);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await projectUserRoleRepository.DeleteByUserIdAsync(userId, transaction);
    }
}
