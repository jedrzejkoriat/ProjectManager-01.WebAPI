using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Queries.GetProjectRolePermissionById;

public record GetProjectRolePermissionByIdQuery() : IRequest<GetProjectRolePermissionByIdResponse>;
