using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.DeleteProjectRole;

public class DeleteProjectRoleHandler : IRequestHandler<DeleteProjectRoleCommand, DeleteProjectRoleResponse>
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRoleService projectRoleService;

    public DeleteProjectRoleHandler(IProjectRoleRepository projectRoleRepository, IProjectRoleService projectRoleService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRoleService = projectRoleService;
    }

    public async Task<DeleteProjectRoleResponse> Handle(DeleteProjectRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
