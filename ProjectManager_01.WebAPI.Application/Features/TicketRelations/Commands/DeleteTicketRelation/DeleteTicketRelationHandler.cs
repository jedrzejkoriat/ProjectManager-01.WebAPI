using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.DeleteTicketRelation;

public class DeleteTicketRelationHandler : IRequestHandler<DeleteTicketRelationCommand, DeleteTicketRelationResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public DeleteTicketRelationHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<DeleteTicketRelationResponse> Handle(DeleteTicketRelationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
