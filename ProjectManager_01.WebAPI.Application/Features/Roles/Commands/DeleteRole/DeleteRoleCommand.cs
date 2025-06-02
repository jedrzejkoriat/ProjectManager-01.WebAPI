using MediatR;

namespace ProjectManager_01.Application.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand() : IRequest<DeleteRoleResponse>;
