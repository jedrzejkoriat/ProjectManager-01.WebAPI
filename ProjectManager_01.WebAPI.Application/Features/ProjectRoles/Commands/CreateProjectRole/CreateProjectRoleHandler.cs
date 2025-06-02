using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRoles.Commands.CreateProjectRole;

public class CreateProjectRoleHandler : IRequestHandler<CreateProjectRoleCommand, CreateProjectRoleResponse>
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRoleService projectRoleService;

    public CreateProjectRoleHandler(IProjectRoleRepository projectRoleRepository, IProjectRoleService projectRoleService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRoleService = projectRoleService;
    }

    public async Task<CreateProjectRoleResponse> Handle(CreateProjectRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
