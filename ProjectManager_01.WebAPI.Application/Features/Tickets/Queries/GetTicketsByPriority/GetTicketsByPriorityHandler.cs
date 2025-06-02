using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByPriority;

public class GetTicketsByPriorityHandler : IRequestHandler<GetTicketsByPriorityQuery, GetTicketsByPriorityResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByPriorityHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByPriorityResponse> Handle(GetTicketsByPriorityQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
