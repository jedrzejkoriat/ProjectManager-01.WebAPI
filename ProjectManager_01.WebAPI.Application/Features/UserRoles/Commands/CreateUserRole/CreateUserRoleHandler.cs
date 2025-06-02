using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.UserRoles.Commands.CreateUserRole;

public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, CreateUserRoleResponse>
{
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IUserRoleService userRoleService;

    public CreateUserRoleHandler(IUserRoleRepository userRoleRepository, IUserRoleService userRoleService)
    {
        this.userRoleRepository = userRoleRepository;
        this.userRoleService = userRoleService;
    }

    public async Task<CreateUserRoleResponse> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
