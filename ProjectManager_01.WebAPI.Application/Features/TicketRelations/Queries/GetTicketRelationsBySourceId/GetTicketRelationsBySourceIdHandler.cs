using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationsBySourceId;

public class GetTicketRelationsBySourceIdHandler : IRequestHandler<GetTicketRelationsBySourceIdQuery, GetTicketRelationsBySourceIdResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public GetTicketRelationsBySourceIdHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<GetTicketRelationsBySourceIdResponse> Handle(GetTicketRelationsBySourceIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
