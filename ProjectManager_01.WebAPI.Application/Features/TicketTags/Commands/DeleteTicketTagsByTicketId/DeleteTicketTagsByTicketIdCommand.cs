using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTagsByTicketId;

public record DeleteTicketTagsByTicketIdCommand() : IRequest<DeleteTicketTagsByTicketIdResponse>;
