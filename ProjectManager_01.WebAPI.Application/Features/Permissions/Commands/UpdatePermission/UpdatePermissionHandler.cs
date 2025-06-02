using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Permissions.Commands.UpdatePermission;

public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, UpdatePermissionResponse>
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IPermissionService permissionService;

    public UpdatePermissionHandler(IPermissionRepository permissionRepository, IPermissionService permissionService)
    {
        this.permissionRepository = permissionRepository;
        this.permissionService = permissionService;
    }

    public async Task<UpdatePermissionResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
