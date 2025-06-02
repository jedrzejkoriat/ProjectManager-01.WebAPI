using MediatR;

namespace ProjectManager_01.Application.Features.UserRoles.Commands.CreateUserRole;

public record CreateUserRoleCommand() : IRequest<CreateUserRoleResponse>;
