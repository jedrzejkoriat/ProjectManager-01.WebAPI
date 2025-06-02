using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Commands.DeleteProjectRolePermission;

public class DeleteProjectRolePermissionHandler : IRequestHandler<DeleteProjectRolePermissionCommand, DeleteProjectRolePermissionResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public DeleteProjectRolePermissionHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<DeleteProjectRolePermissionResponse> Handle(DeleteProjectRolePermissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
