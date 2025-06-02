using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoleByUserId;

public record GetProjectUserRoleByUserIdQuery() : IRequest<GetProjectUserRoleByUserIdResponse>;
