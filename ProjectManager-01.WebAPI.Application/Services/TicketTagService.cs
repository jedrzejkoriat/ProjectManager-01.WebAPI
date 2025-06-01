using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class TicketTagService : ITicketTagService
{
    private readonly ITicketTagRepository ticketTagRepository;

    public TicketTagService(ITicketTagRepository ticketTagRepository)
    {
        this.ticketTagRepository = ticketTagRepository;
    }
}
