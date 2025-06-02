using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Priorities.Commands.CreatePriority;

public class CreatePriorityHandler : IRequestHandler<CreatePriorityCommand, CreatePriorityResponse>
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IPriorityService priorityService;

    public CreatePriorityHandler(IPriorityRepository priorityRepository, IPriorityService priorityService)
    {
        this.priorityRepository = priorityRepository;
        this.priorityService = priorityService;
    }

    public async Task<CreatePriorityResponse> Handle(CreatePriorityCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
