using Microsoft.AspNetCore.Authorization;

namespace ProjectManager_01.Application.Authorization;

public sealed class ProjectPermissionRequirement : IAuthorizationRequirement
{
    public string PermissionName { get; }

    public ProjectPermissionRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }
}