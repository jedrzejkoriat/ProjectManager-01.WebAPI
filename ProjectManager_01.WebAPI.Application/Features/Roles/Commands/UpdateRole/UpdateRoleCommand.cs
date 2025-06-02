using MediatR;

namespace ProjectManager_01.Application.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand() : IRequest<UpdateRoleResponse>;
