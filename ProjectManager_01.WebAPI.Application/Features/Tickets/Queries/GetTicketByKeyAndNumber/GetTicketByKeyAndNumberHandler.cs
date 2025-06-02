using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketByKeyAndNumber;

public class GetTicketByKeyAndNumberHandler : IRequestHandler<GetTicketByKeyAndNumberQuery, GetTicketByKeyAndNumberResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketByKeyAndNumberHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketByKeyAndNumberResponse> Handle(GetTicketByKeyAndNumberQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
