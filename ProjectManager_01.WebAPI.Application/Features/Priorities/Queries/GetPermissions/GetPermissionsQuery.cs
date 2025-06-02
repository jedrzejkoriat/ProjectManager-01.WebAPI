using MediatR;

namespace ProjectManager_01.Application.Features.Priorities.Queries.GetPermissions;

public record GetPermissionsQuery() : IRequest<GetPermissionsResponse>;
