using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationById;

public record GetTicketRelationByIdQuery() : IRequest<GetTicketRelationByIdResponse>;
