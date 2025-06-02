using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Priorities.Commands.DeletePriority;

public class DeletePriorityHandler : IRequestHandler<DeletePriorityCommand, DeletePriorityResponse>
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IPriorityService priorityService;

    public DeletePriorityHandler(IPriorityRepository priorityRepository, IPriorityService priorityService)
    {
        this.priorityRepository = priorityRepository;
        this.priorityService = priorityService;
    }

    public async Task<DeletePriorityResponse> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
