using MediatR;

namespace ProjectManager_01.Application.Features.Permissions.Queries.GetPermissionById;

public record GetPermissionByIdQuery() : IRequest<GetPermissionByIdResponse>;
