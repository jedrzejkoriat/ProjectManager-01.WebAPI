using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Commands.CreateProjectRolePermission;

public class CreateProjectRolePermissionHandler : IRequestHandler<CreateProjectRolePermissionCommand, CreateProjectRolePermissionResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public CreateProjectRolePermissionHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<CreateProjectRolePermissionResponse> Handle(CreateProjectRolePermissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
