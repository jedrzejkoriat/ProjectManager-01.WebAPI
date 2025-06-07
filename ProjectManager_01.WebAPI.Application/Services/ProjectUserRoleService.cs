using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectUserRoleService : IProjectUserRoleService
{
    private readonly IProjectUserRoleRepository _projectUserRoleRepository;
    private readonly IMapper _mapper;

    public ProjectUserRoleService(
        IProjectUserRoleRepository projectUserRoleRepository,
        IMapper mapper)
    {
        _projectUserRoleRepository = projectUserRoleRepository;
        _mapper = mapper;
    }

    public async Task CreateProjectUserRoleAsync(ProjectUserRoleCreateDto projectUserRoleCreateDto)
    {
        var projectUserRole = _mapper.Map<ProjectUserRole>(projectUserRoleCreateDto);
        await _projectUserRoleRepository.CreateAsync(projectUserRole);
    }

    public async Task UpdateProjectUserRoleAsync(ProjectUserRoleUpdateDto projectUserRoleUpdateDto)
    {
        var projectUserRole = _mapper.Map<ProjectUserRole>(projectUserRoleUpdateDto);
        await _projectUserRoleRepository.UpdateAsync(projectUserRole);
    }

    public async Task DeleteProjectUserRoleAsync(Guid projectUserRoleId)
    {
        await _projectUserRoleRepository.DeleteByIdAsync(projectUserRoleId);
    }

    public async Task<ProjectUserRoleDto> GetProjectUserRoleByIdAsync(Guid projectUserRoleId)
    {
        var projectUserRole = await _projectUserRoleRepository.GetByIdAsync(projectUserRoleId);

        return _mapper.Map<ProjectUserRoleDto>(projectUserRole);
    }

    public async Task<IEnumerable<ProjectUserRoleDto>> GetAllProjectUserRolesAsync()
    {
        var projectUserRoles = await _projectUserRoleRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProjectUserRoleDto>>(projectUserRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        await _projectUserRoleRepository.DeleteAllByProjectIdAsync(projectId, transaction);
    }

    public async Task DeleteByProjectRoleId(Guid projectRoleId, IDbTransaction transaction)
    {
        await _projectUserRoleRepository.DeleteAllByProjectRoleIdAsync(projectRoleId, transaction);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await _projectUserRoleRepository.DeleteByUserIdAsync(userId, transaction);
    }

    public async Task<IEnumerable<ProjectUserRoleDto>> GetByUserIdAndProjectIdAsync(Guid userId, Guid projectId)
    {
        var projectUserRole = await _projectUserRoleRepository.GetAllByUserIdAndProjectIdAsync(userId, projectId);

        return _mapper.Map<List<ProjectUserRoleDto>>(projectUserRole);
    }
}
