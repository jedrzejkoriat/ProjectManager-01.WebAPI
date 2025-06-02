using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Priorities.Commands.UpdatePriority;

public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityCommand, UpdatePriorityResponse>
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IPriorityService priorityService;

    public UpdatePriorityHandler(IPriorityRepository priorityRepository, IPriorityService priorityService)
    {
        this.priorityRepository = priorityRepository;
        this.priorityService = priorityService;
    }

    public async Task<UpdatePriorityResponse> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
