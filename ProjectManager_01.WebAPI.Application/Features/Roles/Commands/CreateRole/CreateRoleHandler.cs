using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleService roleService;

    public CreateRoleHandler(IRoleRepository roleRepository, IRoleService roleService)
    {
        this.roleRepository = roleRepository;
        this.roleService = roleService;
    }

    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
