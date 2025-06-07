using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository _projectRoleRepository;
    private readonly IMapper _mapper;
    private readonly IProjectUserRoleService _projectUserRoleService;
    private readonly IDbConnection _dbConnection;
    private readonly IProjectRolePermissionService _projectRolePermissionService;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IMapper mapper,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService)
    {
        _projectRoleRepository = projectRoleRepository;
        _mapper = mapper;
        _projectUserRoleService = projectUserRoleService;
        _dbConnection = dbConnection;
        _projectRolePermissionService = projectRolePermissionService;
    }

    public async Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var projectRole = _mapper.Map<ProjectRole>(projectRoleCreateDto);
            var projectRoleId = await _projectRoleRepository.CreateAsync(projectRole, transaction);

            foreach (var permissionId in projectRoleCreateDto.PermissionIds)
            {
                var projectRolePermissionDto = new ProjectRolePermissionCreateDto(projectRoleId, permissionId);
                await _projectRolePermissionService.CreateProjectRolePermissionAsync(projectRolePermissionDto, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing project role creation transaction.");
        }
    }

    public async Task UpdateProjectRoleAsync(ProjectRoleUpdateDto projectRoleUpdateDto)
    {
        var projectRole = _mapper.Map<ProjectRole>(projectRoleUpdateDto);
        await _projectRoleRepository.UpdateAsync(projectRole);
    }

    public async Task DeleteProjectRoleAsync(Guid projectRoleId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _projectRoleRepository.DeleteByIdAsync(projectRoleId, transaction);
            await _projectUserRoleService.DeleteByProjectRoleId(projectRoleId, transaction);
            await _projectRolePermissionService.DeleteByProjectRoleIdAsync(projectRoleId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing project role deletion transaction.");
        }
    }

    public async Task<ProjectRoleDto> GetProjectRoleByIdAsync(Guid projectRoleId)
    {
        var projectRole = await _projectRoleRepository.GetByIdAsync(projectRoleId);

        return _mapper.Map<ProjectRoleDto>(projectRole);
    }

    public async Task<IEnumerable<ProjectRoleDto>> GetAllProjectRolesAsync()
    {
        var projectRoles = await _projectRoleRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProjectRoleDto>>(projectRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        await _projectRoleRepository.DeleteAllByProjectIdAsync(projectId, transaction);
        await _projectUserRoleService.DeleteByProjectIdAsync(projectId, transaction);
    }
}

