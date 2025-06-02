using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByStatus;

public class GetTicketsByStatusHandler : IRequestHandler<GetTicketsByStatusQuery, GetTicketsByStatusResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByStatusHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByStatusResponse> Handle(GetTicketsByStatusQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
