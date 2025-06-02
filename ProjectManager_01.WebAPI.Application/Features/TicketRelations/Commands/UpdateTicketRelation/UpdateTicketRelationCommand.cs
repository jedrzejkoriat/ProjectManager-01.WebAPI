using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.UpdateTicketRelation;

public record UpdateTicketRelationCommand() : IRequest<UpdateTicketRelationResponse>;
