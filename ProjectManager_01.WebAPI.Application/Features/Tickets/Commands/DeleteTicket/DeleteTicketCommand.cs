using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Commands.DeleteTicket;

public record DeleteTicketCommand() : IRequest<DeleteTicketResponse>;
