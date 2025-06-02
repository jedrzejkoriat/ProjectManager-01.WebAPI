using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Queries.GetProjectRolePermissions;

public class GetProjectRolePermissionsHandler : IRequestHandler<GetProjectRolePermissionsQuery, GetProjectRolePermissionsResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public GetProjectRolePermissionsHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<GetProjectRolePermissionsResponse> Handle(GetProjectRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
