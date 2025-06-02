using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByReporter;

public class GetTicketsByReporterHandler : IRequestHandler<GetTicketsByReporterQuery, GetTicketsByReporterResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public GetTicketsByReporterHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<GetTicketsByReporterResponse> Handle(GetTicketsByReporterQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
