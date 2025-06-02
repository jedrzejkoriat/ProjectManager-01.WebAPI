using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.UserRoles.Commands.DeleteUserRole;

public class DeleteUserRoleHandler : IRequestHandler<DeleteUserRoleCommand, DeleteUserRoleResponse>
{
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IUserRoleService userRoleService;

    public DeleteUserRoleHandler(IUserRoleRepository userRoleRepository, IUserRoleService userRoleService)
    {
        this.userRoleRepository = userRoleRepository;
        this.userRoleService = userRoleService;
    }

    public async Task<DeleteUserRoleResponse> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
