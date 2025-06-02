using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Commands.SoftDeleteTicket;

public class SoftDeleteTicketHandler : IRequestHandler<SoftDeleteTicketCommand, SoftDeleteTicketResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public SoftDeleteTicketHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<SoftDeleteTicketResponse> Handle(SoftDeleteTicketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
