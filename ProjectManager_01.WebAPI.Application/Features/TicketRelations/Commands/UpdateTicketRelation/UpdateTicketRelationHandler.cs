using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.UpdateTicketRelation;

public class UpdateTicketRelationHandler : IRequestHandler<UpdateTicketRelationCommand, UpdateTicketRelationResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public UpdateTicketRelationHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<UpdateTicketRelationResponse> Handle(UpdateTicketRelationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
