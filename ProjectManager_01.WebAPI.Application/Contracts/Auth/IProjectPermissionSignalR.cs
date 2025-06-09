using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ProjectManager_01.Application.Contracts.Auth;
public interface IProjectPermissionSignalR
{
    bool AuthorizeSignalR(HubCallerContext context, string projectId);
}
