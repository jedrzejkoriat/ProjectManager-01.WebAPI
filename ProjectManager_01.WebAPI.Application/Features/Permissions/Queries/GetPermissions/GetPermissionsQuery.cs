using MediatR;

namespace ProjectManager_01.Application.Features.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery() : IRequest<GetPermissionsResponse>;
