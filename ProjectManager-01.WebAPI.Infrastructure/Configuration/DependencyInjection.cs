using Microsoft.Extensions.DependencyInjection;
using ProjectManager_01.WebAPI.Application.Contracts;
using ProjectManager_01.WebAPI.Infrastructure.Repositories;

namespace ProjectManager_01.WebAPI.Infrastructure.Configuration;
internal class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        return services;
    }
}
