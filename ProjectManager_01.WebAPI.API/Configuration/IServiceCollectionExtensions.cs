using System.Reflection;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;

namespace ProjectManager_01.WebAPI.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRequestLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
         {
             options.AddFixedWindowLimiter("fixedlimit", opt =>
              {
                  opt.Window = TimeSpan.FromSeconds(10);
                  opt.PermitLimit = 500;
              });
         });

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorize",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
             });
        });

        return services;
    }
}
