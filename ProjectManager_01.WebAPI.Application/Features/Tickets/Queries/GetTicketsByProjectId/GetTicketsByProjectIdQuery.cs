using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByProjectId;

public record GetTicketsByProjectIdQuery() : IRequest<GetTicketsByProjectIdResponse>;
