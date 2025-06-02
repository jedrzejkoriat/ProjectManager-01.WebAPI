using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Queries.GetProjectRolePermissionById;

public class GetProjectRolePermissionByIdHandler : IRequestHandler<GetProjectRolePermissionByIdQuery, GetProjectRolePermissionByIdResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public GetProjectRolePermissionByIdHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<GetProjectRolePermissionByIdResponse> Handle(GetProjectRolePermissionByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
