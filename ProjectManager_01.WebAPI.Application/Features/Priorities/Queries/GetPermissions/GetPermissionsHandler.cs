using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Priorities.Queries.GetPermissions;

public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, GetPermissionsResponse>
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IPriorityService priorityService;

    public GetPermissionsHandler(IPriorityRepository priorityRepository, IPriorityService priorityService)
    {
        this.priorityRepository = priorityRepository;
        this.priorityService = priorityService;
    }

    public async Task<GetPermissionsResponse> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
