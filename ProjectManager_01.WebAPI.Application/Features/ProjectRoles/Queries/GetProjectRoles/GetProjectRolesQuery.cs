using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRoles.Queries.GetProjectRoles;

public record GetProjectRolesQuery() : IRequest<GetProjectRolesResponse>;
