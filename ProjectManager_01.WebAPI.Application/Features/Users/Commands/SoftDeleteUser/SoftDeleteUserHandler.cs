using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Users.Commands.SoftDeleteUser;

public class SoftDeleteUserHandler : IRequestHandler<SoftDeleteUserCommand, SoftDeleteUserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IUserService userService;

    public SoftDeleteUserHandler(IUserRepository userRepository, IUserService userService)
    {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    public async Task<SoftDeleteUserResponse> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
