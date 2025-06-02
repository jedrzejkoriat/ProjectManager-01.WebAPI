using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTickets;

public record GetTicketsQuery() : IRequest<GetTicketsResponse>;
