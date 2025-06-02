using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRolePermissions.Commands.DeleteProjectRolePermissionByPermissionId;

public class DeleteProjectRolePermissionByPermissionIdHandler : IRequestHandler<DeleteProjectRolePermissionByPermissionIdCommand, DeleteProjectRolePermissionByPermissionIdResponse>
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;
    private readonly IProjectRolePermissionService projectRolePermissionService;

    public DeleteProjectRolePermissionByPermissionIdHandler(IProjectRolePermissionRepository projectRolePermissionRepository, IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
        this.projectRolePermissionService = projectRolePermissionService;
    }

    public async Task<DeleteProjectRolePermissionByPermissionIdResponse> Handle(DeleteProjectRolePermissionByPermissionIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
