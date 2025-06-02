using MediatR;

namespace ProjectManager_01.Application.Features.Permissions.Commands.DeletePermission;

public record DeletePermissionCommand() : IRequest<DeletePermissionResponse>;
