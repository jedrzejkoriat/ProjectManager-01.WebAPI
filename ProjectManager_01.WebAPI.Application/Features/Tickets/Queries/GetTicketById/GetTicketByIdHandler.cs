using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketById;

public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, GetTicketByIdResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketByIdHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketByIdResponse> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
