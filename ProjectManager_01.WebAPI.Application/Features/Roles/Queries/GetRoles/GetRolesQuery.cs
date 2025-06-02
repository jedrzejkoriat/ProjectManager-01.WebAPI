using MediatR;

namespace ProjectManager_01.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery() : IRequest<GetRolesResponse>;
