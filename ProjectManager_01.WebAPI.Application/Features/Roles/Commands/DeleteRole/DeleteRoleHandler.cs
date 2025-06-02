using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleService roleService;

    public DeleteRoleHandler(IRoleRepository roleRepository, IRoleService roleService)
    {
        this.roleRepository = roleRepository;
        this.roleService = roleService;
    }

    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
