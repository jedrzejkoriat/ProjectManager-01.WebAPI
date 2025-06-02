using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoleById;

public record GetProjectUserRoleByIdQuery() : IRequest<GetProjectUserRoleByIdResponse>;
