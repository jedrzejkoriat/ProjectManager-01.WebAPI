using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByProjectId;

public class GetTicketsByProjectIdHandler : IRequestHandler<GetTicketsByProjectIdQuery, GetTicketsByProjectIdResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByProjectIdHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByProjectIdResponse> Handle(GetTicketsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
