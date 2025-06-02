using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Priorities.Queries.GetPermissionById;

public class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQuery, GetPermissionByIdResponse>
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IPriorityService priorityService;

    public GetPermissionByIdHandler(IPriorityRepository priorityRepository, IPriorityService priorityService)
    {
        this.priorityRepository = priorityRepository;
        this.priorityService = priorityService;
    }

    public async Task<GetPermissionByIdResponse> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
