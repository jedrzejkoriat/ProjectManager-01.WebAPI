using MediatR;

namespace ProjectManager_01.Application.Features.UserRoles.Queries.GetUserRoleByUserId;

public record GetUserRoleByUserIdQuery() : IRequest<GetUserRoleByUserIdResponse>;
