using Microsoft.AspNetCore.Authorization;

namespace ProjectManager_01.Application.Auth;

public sealed class ProjectPermissionRequirement : IAuthorizationRequirement
{
    public string PermissionName { get; }

    public ProjectPermissionRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }
}