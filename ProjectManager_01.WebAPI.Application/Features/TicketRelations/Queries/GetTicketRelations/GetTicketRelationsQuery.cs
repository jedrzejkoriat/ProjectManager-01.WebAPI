using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelations;

public record GetTicketRelationsQuery() : IRequest<GetTicketRelationsResponse>;
