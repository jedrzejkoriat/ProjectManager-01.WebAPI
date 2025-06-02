using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByStatus;

public record GetTicketsByStatusQuery() : IRequest<GetTicketsByStatusResponse>;
