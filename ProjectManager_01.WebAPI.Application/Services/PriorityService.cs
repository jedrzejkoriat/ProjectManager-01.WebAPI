using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository priorityRepository;

    public PriorityService(IPriorityRepository priorityRepository)
    {
        this.priorityRepository = priorityRepository;
    }
}
