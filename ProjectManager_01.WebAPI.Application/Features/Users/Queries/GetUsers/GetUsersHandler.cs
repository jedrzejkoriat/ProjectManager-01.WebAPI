using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Users.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IUserService userService;

    public GetUsersHandler(IUserRepository userRepository, IUserService userService)
    {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
