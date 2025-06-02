using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByTicketType;

public class GetTicketsByTicketTypeHandler : IRequestHandler<GetTicketsByTicketTypeQuery, GetTicketsByTicketTypeResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByTicketTypeHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByTicketTypeResponse> Handle(GetTicketsByTicketTypeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
