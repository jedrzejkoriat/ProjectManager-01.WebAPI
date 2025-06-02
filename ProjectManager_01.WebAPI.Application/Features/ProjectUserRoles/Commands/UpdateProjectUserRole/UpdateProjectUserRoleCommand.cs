using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Commands.UpdateProjectUserRole;

public record UpdateProjectUserRoleCommand() : IRequest<UpdateProjectUserRoleResponse>;
