using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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