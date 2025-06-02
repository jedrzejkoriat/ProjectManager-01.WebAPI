using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Queries.GetProjectRolePermissions;

public record GetProjectRolePermissionsQuery() : IRequest<GetProjectRolePermissionsResponse>;
