using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Commands.DeleteTicket;

public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, DeleteTicketResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public DeleteTicketHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<DeleteTicketResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
