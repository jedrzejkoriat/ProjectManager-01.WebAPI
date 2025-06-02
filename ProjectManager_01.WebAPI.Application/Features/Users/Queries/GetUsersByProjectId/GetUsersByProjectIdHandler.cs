using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Users.Queries.GetUsersByProjectId;

public class GetUsersByProjectIdHandler : IRequestHandler<GetUsersByProjectIdQuery, GetUsersByProjectIdResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IUserService userService;

    public GetUsersByProjectIdHandler(IUserRepository userRepository, IUserService userService)
    {
        this.userRepository = userRepository;
        this.userService = userService;
    }

    public async Task<GetUsersByProjectIdResponse> Handle(GetUsersByProjectIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
