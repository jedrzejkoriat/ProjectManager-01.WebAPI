using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Permissions.Queries.GetPermissions;

public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, GetPermissionsResponse>
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IPermissionService permissionService;

    public GetPermissionsHandler(IPermissionRepository permissionRepository, IPermissionService permissionService)
    {
        this.permissionRepository = permissionRepository;
        this.permissionService = permissionService;
    }

    public async Task<GetPermissionsResponse> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
