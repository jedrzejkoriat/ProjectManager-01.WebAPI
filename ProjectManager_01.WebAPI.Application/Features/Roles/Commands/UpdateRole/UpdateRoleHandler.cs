using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleService roleService;

    public UpdateRoleHandler(IRoleRepository roleRepository, IRoleService roleService)
    {
        this.roleRepository = roleRepository;
        this.roleService = roleService;
    }

    public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
