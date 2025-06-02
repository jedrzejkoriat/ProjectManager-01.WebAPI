using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Permissions.Commands.CreatePermission;

public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, CreatePermissionResponse>
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IPermissionService permissionService;

    public CreatePermissionHandler(IPermissionRepository permissionRepository, IPermissionService permissionService)
    {
        this.permissionRepository = permissionRepository;
        this.permissionService = permissionService;
    }

    public async Task<CreatePermissionResponse> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
