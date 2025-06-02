using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.UserRoles.Queries.GetUserRoleByUserId;

public class GetUserRoleByUserIdHandler : IRequestHandler<GetUserRoleByUserIdQuery, GetUserRoleByUserIdResponse>
{
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IUserRoleService userRoleService;

    public GetUserRoleByUserIdHandler(IUserRoleRepository userRoleRepository, IUserRoleService userRoleService)
    {
        this.userRoleRepository = userRoleRepository;
        this.userRoleService = userRoleService;
    }

    public async Task<GetUserRoleByUserIdResponse> Handle(GetUserRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
