using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Queries.GetTicketRelationsByTargetId;

public record GetTicketRelationsByTargetIdQuery() : IRequest<GetTicketRelationsByTargetIdResponse>;
