using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.DeleteProjectRole;

public record DeleteProjectRoleCommand() : IRequest<DeleteProjectRoleResponse>;
