using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Permissions.Queries.GetPermissionById;

public class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQuery, GetPermissionByIdResponse>
{
    private readonly IPermissionRepository permissionRepository;
    private readonly IPermissionService permissionService;

    public GetPermissionByIdHandler(IPermissionRepository permissionRepository, IPermissionService permissionService)
    {
        this.permissionRepository = permissionRepository;
        this.permissionService = permissionService;
    }

    public async Task<GetPermissionByIdResponse> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
