using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRoles.Queries.GetProjectRoleById;

public record GetProjectRoleByIdQuery() : IRequest<GetProjectRoleByIdResponse>;
