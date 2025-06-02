using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.UpdateProjectRole;

public class UpdateProjectRoleHandler : IRequestHandler<UpdateProjectRoleCommand, UpdateProjectRoleResponse>
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRoleService projectRoleService;

    public UpdateProjectRoleHandler(IProjectRoleRepository projectRoleRepository, IProjectRoleService projectRoleService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRoleService = projectRoleService;
    }

    public async Task<UpdateProjectRoleResponse> Handle(UpdateProjectRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
