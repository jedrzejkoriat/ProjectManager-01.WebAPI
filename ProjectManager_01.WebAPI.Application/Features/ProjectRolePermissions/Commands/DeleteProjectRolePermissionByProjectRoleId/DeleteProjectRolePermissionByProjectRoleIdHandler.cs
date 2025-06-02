using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Commands.DeleteProjectRolePermissionByProjectRoleId;

public class DeleteProjectRolePermissionByProjectRoleIdHandler : IRequestHandler<DeleteProjectRolePermissionByProjectRoleIdCommand, DeleteProjectRolePermissionByProjectRoleIdResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public DeleteProjectRolePermissionByProjectRoleIdHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<DeleteProjectRolePermissionByProjectRoleIdResponse> Handle(DeleteProjectRolePermissionByProjectRoleIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
