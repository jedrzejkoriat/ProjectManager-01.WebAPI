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
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IMapper mapper;
    private readonly IProjectUserRoleService projectUserRoleService;
    private readonly IDbConnection dbConnection;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public ProjectRoleService(
        IProjectRoleRepository projectRoleRepository,
        IMapper mapper,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.mapper = mapper;
        this.projectUserRoleService = projectUserRoleService;
        this.dbConnection = dbConnection;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task CreateProjectRoleAsync(ProjectRoleCreateDto projectRoleCreateDto)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(dbConnection);

        try
        {
            var projectRole = mapper.Map<ProjectRole>(projectRoleCreateDto);
            var projectRoleId = await projectRoleRepository.CreateAsync(projectRole, transaction);

            foreach (var permissionId in projectRoleCreateDto.PermissionIds)
            {
                var projectRolePermissionDto = new ProjectRolePermissionCreateDto(projectRoleId, permissionId);
                await projectRolePermissionService.CreateProjectRolePermissionAsync(projectRolePermissionDto, transaction);
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
        var projectRole = mapper.Map<ProjectRole>(projectRoleUpdateDto);
        await projectRoleRepository.UpdateAsync(projectRole);
    }

    public async Task DeleteProjectRoleAsync(Guid projectRoleId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(dbConnection);

        try
        {
            await projectRoleRepository.DeleteAsync(projectRoleId, transaction);
            await projectUserRoleService.DeleteByProjectRoleId(projectRoleId, transaction);
            await projectRolePermissionService.DeleteByProjectRoleIdAsync(projectRoleId, transaction);

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
        var projectRole = await projectRoleRepository.GetByIdAsync(projectRoleId);

        return mapper.Map<ProjectRoleDto>(projectRole);
    }

    public async Task<IEnumerable<ProjectRoleDto>> GetAllProjectRolesAsync()
    {
        var projectRoles = await projectRoleRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProjectRoleDto>>(projectRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        await projectRoleRepository.DeleteByProjectIdAsync(projectId, transaction);
        await projectUserRoleService.DeleteByProjectIdAsync(projectId, transaction);
    }
}

