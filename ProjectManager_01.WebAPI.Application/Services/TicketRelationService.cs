using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;

public class TicketRelationService : ITicketRelationService
{
    private readonly ITicketRelationRepository ticketRelationRepository;

    public TicketRelationService(ITicketRelationRepository ticketRelationRepository)
    {
        this.ticketRelationRepository = ticketRelationRepository;
    }
}
