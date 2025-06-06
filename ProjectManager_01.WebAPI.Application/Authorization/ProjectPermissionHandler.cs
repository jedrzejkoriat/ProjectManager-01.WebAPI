using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ProjectManager_01.Application.Authorization;

public sealed class ProjectPermissionHandler : AuthorizationHandler<ProjectPermissionRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectPermissionHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectPermissionRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var routeData = httpContext?.GetRouteData();
        var projectId = routeData?.Values["projectId"]?.ToString().ToLower();

        if (string.IsNullOrWhiteSpace(projectId))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var permissionName = requirement.PermissionName;

        var claims = context.User.FindAll("ProjectPermission");

        Console.WriteLine(claims);

        var hasPermission = claims.Any(c =>
        {
            var parts = c.Value.Split(':');
            return parts.Length == 2 && parts[0] == projectId && parts[1].ToLower() == requirement.PermissionName;
        });

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}