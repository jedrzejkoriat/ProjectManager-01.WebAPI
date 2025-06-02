using MediatR;

namespace ProjectManager_01.Application.Features.UserRoles.Queries.GetUserRolesByRoleId;

public record GetUserRolesByRoleIdQuery() : IRequest<GetUserRolesByRoleIdResponse>;
