using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectManager_01.Application.Auth;
using ProjectManager_01.Application.Contracts.Auth;
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

    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthorizationHandler, ProjectPermissionHandler>();
        services.AddScoped<IProjectAccessValidator, ProjectAccessValidator>();
        services.AddScoped<IProjectPermissionSignalR, ProjectPermissionSignalR>();
        services.AddScoped<IUserAccessValidator, UserAccessValidator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/tickets"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });

        services.AddAuthorization(options =>
        {
            AuthorizationPolicies.RegisterPolicies(options);
        });

        return services;
    }

    public static IServiceCollection AddApplicationMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());
        return services;
    }

    public static IServiceCollection AddDtoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.Load("ProjectManager_01.Application"));

        return services;
    }
}
