using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, CreateTicketResponse>
{
    private readonly ITicketRepository ticketRepository;
    private readonly ITicketService ticketService;

    public CreateTicketHandler(ITicketRepository ticketRepository, ITicketService ticketService)
    {
        this.ticketRepository = ticketRepository;
        this.ticketService = ticketService;
    }

    public async Task<CreateTicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
