using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelations;

public class GetTicketRelationsHandler : IRequestHandler<GetTicketRelationsQuery, GetTicketRelationsResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public GetTicketRelationsHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<GetTicketRelationsResponse> Handle(GetTicketRelationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
