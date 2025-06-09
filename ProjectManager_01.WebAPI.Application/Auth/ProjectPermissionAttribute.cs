using Microsoft.AspNetCore.Authorization;

namespace ProjectManager_01.Application.Auth;

public sealed class ProjectPermissionAttribute : AuthorizeAttribute
{
    public ProjectPermissionAttribute(string permissionName)
    {
        Policy = $"ProjectPermission:{permissionName}";
    }
}