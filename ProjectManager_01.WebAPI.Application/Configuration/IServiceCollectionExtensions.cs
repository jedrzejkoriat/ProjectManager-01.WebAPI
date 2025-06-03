using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.Services;

namespace ProjectManager_01.Application.Configuration;

public static class IServiceCollectionExtensions
{
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

    public static IServiceCollection AddApplicationMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());
        return services;
    }
}
