using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Commands.UpdateTicket;

public record UpdateTicketCommand() : IRequest<UpdateTicketResponse>;
