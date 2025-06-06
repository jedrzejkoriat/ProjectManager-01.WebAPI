using Microsoft.AspNetCore.Authorization;
using ProjectManager_01.Application.Constants;

namespace ProjectManager_01.Application.Authorization;

public static class AuthorizationPolicies
{
    public static void RegisterPolicies(AuthorizationOptions options)
    {
        var allPermissions = new[]
        {
            Permissions.ReadComment,
            Permissions.WriteComment,
            Permissions.ReadTag,
            Permissions.WriteTag,
            Permissions.ReadTicket,
            Permissions.WriteTicket,
            Permissions.DeleteTicket,
            Permissions.ReadTicketRelation,
            Permissions.WriteTicketRelation,
            Permissions.ReadTicketTag,
            Permissions.WriteTicketTag
        };

        foreach (var permission in allPermissions)
        {
            options.AddPolicy(permission, policy =>
            {
                policy.Requirements.Add(new ProjectPermissionRequirement(permission));
            });
        }
    }
}