using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRolePermissionService : IProjectRolePermissionService
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IMapper mapper;

    public ProjectRolePermissionService(
        IProjectRolePermissionRepository projectRolePermissionRepository,
        IMapper mapper)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.mapper = mapper;
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto)
    {
        var projectRolePermission = mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);
        await projectRolePermissionRepository.CreateAsync(projectRolePermission);
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto, IDbTransaction transaction)
    {
        var projectRolePermission = mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);
        await projectRolePermissionRepository.CreateAsync(projectRolePermission, transaction);
    }

    public async Task DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction)
    {
        await projectRolePermissionRepository.DeleteByPermissionIdAsync(permissionId, transaction);
    }

    public async Task DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        await projectRolePermissionRepository.DeleteByProjectRoleIdAsync(projectRoleId, transaction);
    }

    public async Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId)
    {
        await projectRolePermissionRepository.DeleteAsync(projectRoleId, permissionId);
    }

    public async Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId)
    {
        var projectRolePermission = await projectRolePermissionRepository.GetByIdAsync(projectRoleId, permissionId);

        return mapper.Map<ProjectRolePermissionDto>(projectRolePermission);
    }

    public async Task<IEnumerable<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync()
    {
        var projectRolePermissions = await projectRolePermissionRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProjectRolePermissionDto>>(projectRolePermissions);
    }
}
