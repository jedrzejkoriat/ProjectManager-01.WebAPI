using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.UpdateProjectRole;

public record UpdateProjectRoleCommand() : IRequest<UpdateProjectRoleResponse>;
