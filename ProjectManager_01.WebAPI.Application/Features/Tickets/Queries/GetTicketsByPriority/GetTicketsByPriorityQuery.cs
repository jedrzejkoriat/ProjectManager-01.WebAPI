using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByPriority;

public record GetTicketsByPriorityQuery() : IRequest<GetTicketsByPriorityResponse>;
