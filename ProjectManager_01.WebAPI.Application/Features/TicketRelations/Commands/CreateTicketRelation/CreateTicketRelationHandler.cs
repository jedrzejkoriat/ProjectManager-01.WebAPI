using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.CreateTicketRelation;

public class CreateTicketRelationHandler : IRequestHandler<CreateTicketRelationCommand, CreateTicketRelationResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public CreateTicketRelationHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<CreateTicketRelationResponse> Handle(CreateTicketRelationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
