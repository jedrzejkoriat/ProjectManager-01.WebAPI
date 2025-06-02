using MediatR;

namespace ProjectManager_01.Application.Features.Permissions.Commands.UpdatePermission;

public record UpdatePermissionCommand() : IRequest<UpdatePermissionResponse>;
