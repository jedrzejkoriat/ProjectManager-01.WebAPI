using Microsoft.Extensions.DependencyInjection;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.Services;
using ProjectManager_01.Infrastructure.Repositories;

namespace ProjectManager_01.Infrastructure.Configuration;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPriorityRepository, PriorityRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectRolePermissionRepository, ProjectRolePermissionRepository>();
        services.AddScoped<IProjectRoleRepository, ProjectRoleRepository>();
        services.AddScoped<IProjectUserRoleRepository, ProjectUserRoleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITicketRelationRepository, TicketRelationRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketTagRepository, TicketTagRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPriorityService, PriorityService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectRolePermissionService, ProjectRolePermissionService>();
        services.AddScoped<IProjectRoleService, ProjectRoleService>();
        services.AddScoped<IProjectUserRoleService, ProjectUserRoleService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ITicketRelationService, TicketRelationService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITicketTagService, TicketTagService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }
}
