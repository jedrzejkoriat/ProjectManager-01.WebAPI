using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Auth;

namespace ProjectManager_01.Application.Auth;
public sealed class ProjectPermissionSignalR : IProjectPermissionSignalR
{
    public bool AuthorizeSignalR(HubCallerContext context, string projectId)
    {
        var claims = context?.User?.FindAll("ProjectPermission");

        var hasPermission = claims.Any(c =>
        {
            var parts = c.Value.Split(':');
            return parts.Length == 2 && parts[0] == projectId && parts[1].Equals(Permissions.ReadTicket, StringComparison.OrdinalIgnoreCase);
        });

        if (hasPermission)
            return true;
        else
            return false;
    }
}