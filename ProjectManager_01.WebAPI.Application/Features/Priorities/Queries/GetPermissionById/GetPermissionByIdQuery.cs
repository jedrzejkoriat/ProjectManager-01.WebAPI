using MediatR;

namespace ProjectManager_01.Application.Features.Priorities.Queries.GetPermissionById;

public record GetPermissionByIdQuery() : IRequest<GetPermissionByIdResponse>;
