using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.CreateTicketRelation;

public record CreateTicketRelationCommand() : IRequest<CreateTicketRelationResponse>;
