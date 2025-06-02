using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationsByTargetId;

public class GetTicketRelationsByTargetIdHandler : IRequestHandler<GetTicketRelationsByTargetIdQuery, GetTicketRelationsByTargetIdResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public GetTicketRelationsByTargetIdHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<GetTicketRelationsByTargetIdResponse> Handle(GetTicketRelationsByTargetIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
