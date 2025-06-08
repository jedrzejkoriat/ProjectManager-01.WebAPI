using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectManager_01.Application.Constants;

namespace ProjectManager_01.Application.Auth;

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
        var projectId = routeData?.Values["projectId"]?.ToString();

        if (string.IsNullOrWhiteSpace(projectId))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        Guid.TryParse(projectId, out var parsedProjectId);

        var permissionName = requirement.PermissionName;

        var claims = context.User.FindAll("ProjectPermission");

        var hasPermission = claims.Any(c =>
        {
            var parts = c.Value.Split(':');
            return parts.Length == 2 && Guid.Parse(parts[0]) == parsedProjectId && parts[1].Equals(requirement.PermissionName);
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