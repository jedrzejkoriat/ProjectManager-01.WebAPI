using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByResolution;

public class GetTicketsByResolutionHandler : IRequestHandler<GetTicketsByResolutionQuery, GetTicketsByResolutionResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByResolutionHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByResolutionResponse> Handle(GetTicketsByResolutionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
