using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTickets;

public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, GetTicketsResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
