using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByTicketType;

public record GetTicketsByTicketTypeQuery() : IRequest<GetTicketsByTicketTypeResponse>;
