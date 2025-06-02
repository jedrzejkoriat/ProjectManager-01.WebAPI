using MediatR;

namespace ProjectManager_01.Application.Features.TicketRelations.Commands.DeleteTicketRelation;

public record DeleteTicketRelationCommand() : IRequest<DeleteTicketRelationResponse>;
