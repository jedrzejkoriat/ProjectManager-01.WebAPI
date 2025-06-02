using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Permissions.Commands.DeletePermission;

public class DeletePermissionHandler : IRequestHandler<DeletePermissionCommand, DeletePermissionResponse>
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IPermissionService permissionService;

    public DeletePermissionHandler(IPermissionRepository permissionRepository, IPermissionService permissionService)
    {
        this.permissionRepository = permissionRepository;
        this.permissionService = permissionService;
    }

    public async Task<DeletePermissionResponse> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
