using MediatR;

namespace ProjectManager_01.Application.Features.Permissions.Commands.CreatePermission;

public record CreatePermissionCommand() : IRequest<CreatePermissionResponse>;
