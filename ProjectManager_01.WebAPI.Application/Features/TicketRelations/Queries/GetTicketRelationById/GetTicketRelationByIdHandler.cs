using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationById;

public class GetTicketRelationByIdHandler : IRequestHandler<GetTicketRelationByIdQuery, GetTicketRelationByIdResponse>
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly ITicketRelationService ticketRelationService;

    public GetTicketRelationByIdHandler(ITicketRelationRepository ticketRelationRepository, ITicketRelationService ticketRelationService)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.ticketRelationService = ticketRelationService;
    }

    public async Task<GetTicketRelationByIdResponse> Handle(GetTicketRelationByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
