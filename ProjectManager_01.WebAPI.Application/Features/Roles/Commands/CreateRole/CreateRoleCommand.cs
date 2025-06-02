using MediatR;

namespace ProjectManager_01.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand() : IRequest<CreateRoleResponse>;
