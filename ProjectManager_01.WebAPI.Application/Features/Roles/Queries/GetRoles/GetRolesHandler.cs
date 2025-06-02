using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Roles.Queries.GetRoles;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, GetRolesResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleService roleService;

    public GetRolesHandler(IRoleRepository roleRepository, IRoleService roleService)
    {
        this.roleRepository = roleRepository;
        this.roleService = roleService;
    }

    public async Task<GetRolesResponse> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
