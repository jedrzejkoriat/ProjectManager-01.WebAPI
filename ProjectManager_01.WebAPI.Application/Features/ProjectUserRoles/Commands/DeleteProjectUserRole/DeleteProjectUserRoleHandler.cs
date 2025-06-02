using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Commands.DeleteProjectUserRole;

public class DeleteProjectUserRoleHandler : IRequestHandler<DeleteProjectUserRoleCommand, DeleteProjectUserRoleResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public DeleteProjectUserRoleHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<DeleteProjectUserRoleResponse> Handle(DeleteProjectUserRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
