using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleService roleService;

    public GetRoleByIdHandler(IRoleRepository roleRepository, IRoleService roleService)
    {
        this.roleRepository = roleRepository;
        this.roleService = roleService;
    }

    public async Task<GetRoleByIdResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
