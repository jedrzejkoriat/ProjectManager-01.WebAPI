using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRolePermissionService : IProjectRolePermissionService
{
    private readonly IProjectRolePermissionRepository _projectRolePermissionRepository;
    private readonly IMapper _mapper;

    public ProjectRolePermissionService(
        IProjectRolePermissionRepository projectRolePermissionRepository,
        IMapper mapper)
    {
        _projectRolePermissionRepository = projectRolePermissionRepository;
        _mapper = mapper;
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto)
    {
        var projectRolePermission = _mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);
        await _projectRolePermissionRepository.CreateAsync(projectRolePermission);
    }

    public async Task CreateProjectRolePermissionAsync(ProjectRolePermissionCreateDto projectRolePermissionCreateDto, IDbTransaction transaction)
    {
        var projectRolePermission = _mapper.Map<ProjectRolePermission>(projectRolePermissionCreateDto);
        await _projectRolePermissionRepository.CreateAsync(projectRolePermission, transaction);
    }

    public async Task DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction)
    {
        await _projectRolePermissionRepository.DeleteByPermissionIdAsync(permissionId, transaction);
    }

    public async Task DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        await _projectRolePermissionRepository.DeleteByProjectRoleIdAsync(projectRoleId, transaction);
    }

    public async Task DeleteProjectRolePermissionAsync(Guid projectRoleId, Guid permissionId)
    {
        await _projectRolePermissionRepository.DeleteAsync(projectRoleId, permissionId);
    }

    public async Task<ProjectRolePermissionDto> GetProjectRolePermissionByIdAsync(Guid projectRoleId, Guid permissionId)
    {
        var projectRolePermission = await _projectRolePermissionRepository.GetByIdAsync(projectRoleId, permissionId);

        return _mapper.Map<ProjectRolePermissionDto>(projectRolePermission);
    }

    public async Task<IEnumerable<ProjectRolePermissionDto>> GetProjectRolePermissionsAsync()
    {
        var projectRolePermissions = await _projectRolePermissionRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProjectRolePermissionDto>>(projectRolePermissions);
    }
}
