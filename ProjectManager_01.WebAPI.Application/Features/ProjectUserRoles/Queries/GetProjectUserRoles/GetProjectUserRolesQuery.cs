using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoles;

public record GetProjectUserRolesQuery() : IRequest<GetProjectUserRolesResponse>;
