using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByAssignee;

public record GetTicketsByAssigneeQuery() : IRequest<GetTicketsByAssigneeResponse>;
