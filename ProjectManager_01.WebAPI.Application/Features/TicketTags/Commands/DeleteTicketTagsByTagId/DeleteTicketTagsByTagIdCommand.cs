using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTagsByTagId;

public record DeleteTicketTagsByTagIdCommand() : IRequest<DeleteTicketTagsByTagIdResponse>;
