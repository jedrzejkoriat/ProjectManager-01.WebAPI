using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Commands.CreateProjectUserRole;

public class CreateProjectUserRoleHandler : IRequestHandler<CreateProjectUserRoleCommand, CreateProjectUserRoleResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public CreateProjectUserRoleHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<CreateProjectUserRoleResponse> Handle(CreateProjectUserRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
