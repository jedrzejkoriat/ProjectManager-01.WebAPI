using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.CreateTicketTag;

public record CreateTicketTagCommand() : IRequest<CreateTicketTagResponse>;
