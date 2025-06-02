using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Commands.CreateProjectRolePermission;

public record CreateProjectRolePermissionCommand() : IRequest<CreateProjectRolePermissionResponse>;
