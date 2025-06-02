using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Commands.UpdateTicket;

public class UpdateTicketHandler : IRequestHandler<UpdateTicketCommand, UpdateTicketResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public UpdateTicketHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<UpdateTicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
