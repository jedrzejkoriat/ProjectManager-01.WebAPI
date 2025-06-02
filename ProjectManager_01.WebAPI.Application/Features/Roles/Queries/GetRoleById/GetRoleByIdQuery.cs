using MediatR;

namespace ProjectManager_01.Application.Features.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery() : IRequest<GetRoleByIdResponse>;
