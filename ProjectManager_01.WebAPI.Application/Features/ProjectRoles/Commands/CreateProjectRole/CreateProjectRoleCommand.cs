using MediatR;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.CreateProjectRole;

public record CreateProjectRoleCommand() : IRequest<CreateProjectRoleResponse>;
