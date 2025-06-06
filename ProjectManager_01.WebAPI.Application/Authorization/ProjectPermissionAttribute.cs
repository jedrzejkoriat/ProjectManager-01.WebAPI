using Microsoft.AspNetCore.Authorization;

namespace ProjectManager_01.Application.Authorization;

public sealed class ProjectPermissionAttribute : AuthorizeAttribute
{
    public ProjectPermissionAttribute(string permissionName)
    {
        Policy = $"ProjectPermission:{permissionName}";
    }
}