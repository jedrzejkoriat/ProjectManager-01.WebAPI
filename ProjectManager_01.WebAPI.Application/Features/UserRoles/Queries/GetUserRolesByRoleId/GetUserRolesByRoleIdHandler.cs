using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.UserRoles.Queries.GetUserRolesByRoleId;

public class GetUserRolesByRoleIdHandler : IRequestHandler<GetUserRolesByRoleIdQuery, GetUserRolesByRoleIdResponse>
{
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IUserRoleService userRoleService;

    public GetUserRolesByRoleIdHandler(IUserRoleRepository userRoleRepository, IUserRoleService userRoleService)
    {
        this.userRoleRepository = userRoleRepository;
        this.userRoleService = userRoleService;
    }

    public async Task<GetUserRolesByRoleIdResponse> Handle(GetUserRolesByRoleIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
