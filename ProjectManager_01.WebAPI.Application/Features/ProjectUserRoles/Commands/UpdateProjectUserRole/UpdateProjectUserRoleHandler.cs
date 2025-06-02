using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Commands.UpdateProjectUserRole;

public class UpdateProjectUserRoleHandler : IRequestHandler<UpdateProjectUserRoleCommand, UpdateProjectUserRoleResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public UpdateProjectUserRoleHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<UpdateProjectUserRoleResponse> Handle(UpdateProjectUserRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
