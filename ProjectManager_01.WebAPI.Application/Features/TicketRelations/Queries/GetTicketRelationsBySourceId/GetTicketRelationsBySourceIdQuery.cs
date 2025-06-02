using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationsBySourceId;

public record GetTicketRelationsBySourceIdQuery() : IRequest<GetTicketRelationsBySourceIdResponse>;
