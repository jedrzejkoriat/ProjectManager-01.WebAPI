using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class ProjectRoleService : IProjectRoleService
{

    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IMapper mapper;
    private readonly IProjectUserRoleService projectUserRoleService;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public ProjectRoleService(IProjectRoleRepository projectRoleRepository, 
        IMapper mapper, 
        IProjectUserRoleService projectUserRoleService, 
        IDbConnectionFactory dbConnectionFactory,
        IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.mapper = mapper;
        this.projectUserRoleService = projectUserRoleService;
        this.dbConnectionFactory = dbConnectionFactory;
        this.projectRolePermissionService = projectRolePermissionService;
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
        using var connection = dbConnectionFactory.CreateConnection();

        switch (connection)
        {
            case SqlConnection sqlConnection:
                await sqlConnection.OpenAsync();
                break;
            default:
                connection.Open();
                break;
        }

        using var transaction = connection.BeginTransaction();

        try
        {
            await projectRoleRepository.DeleteAsync(projectRoleId, connection, transaction);
            await projectUserRoleService.DeleteByProjectRoleId(projectRoleId, connection, transaction);
            await projectRolePermissionService.DeleteByProjectRoleIdAsync(projectRoleId, connection, transaction);

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
        ProjectRole projectRole = await projectRoleRepository.GetByIdAsync(projectRoleId);

        return mapper.Map<ProjectRoleDto>(projectRole);
    }

    public async Task<List<ProjectRoleDto>> GetAllProjectRolesAsync()
    {
        List<ProjectRole> projectRoles = await projectRoleRepository.GetAllAsync();

        return mapper.Map<List<ProjectRoleDto>>(projectRoles);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction)
    {
        await projectRoleRepository.DeleteByProjectIdAsync(projectId, connection, transaction);
        await projectUserRoleService.DeleteByProjectIdAsync(projectId, connection, transaction);
    }
}

