using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTag;

public record DeleteTicketTagCommand() : IRequest<DeleteTicketTagResponse>;
