using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Commands.SoftDeleteTicket;

public record SoftDeleteTicketCommand() : IRequest<SoftDeleteTicketResponse>;
