using MediatR;

namespace ProjectManager_01.Application.Features.UserRoles.Commands.DeleteUserRole;

public record DeleteUserRoleCommand() : IRequest<DeleteUserRoleResponse>;
