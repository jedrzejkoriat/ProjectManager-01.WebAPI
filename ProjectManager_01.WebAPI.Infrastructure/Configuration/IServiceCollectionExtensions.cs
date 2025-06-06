using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager_01.Application.Contracts.Authorization;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Infrastructure.Auth;
using ProjectManager_01.Infrastructure.Repositories;

namespace ProjectManager_01.Infrastructure.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDapperRepositories(this IServiceCollection services)
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

    public static IServiceCollection AddJwtGenerator(this IServiceCollection services)
    {
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        return services;
    }

    public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddScoped<IDbConnection>(sp =>
        {
            return new SqlConnection(connectionString);
        });

        return services;
    }
}
