using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Commands.CreateTicket;

public record CreateTicketCommand() : IRequest<CreateTicketResponse>;
