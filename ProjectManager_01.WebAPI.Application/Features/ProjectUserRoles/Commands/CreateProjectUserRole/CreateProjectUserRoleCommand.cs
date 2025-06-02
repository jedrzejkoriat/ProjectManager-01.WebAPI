using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Commands.CreateProjectUserRole;

public record CreateProjectUserRoleCommand() : IRequest<CreateProjectUserRoleResponse>;
