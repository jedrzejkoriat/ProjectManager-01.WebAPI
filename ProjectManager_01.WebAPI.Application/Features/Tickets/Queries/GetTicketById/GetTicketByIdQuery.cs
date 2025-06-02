using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketById;

public record GetTicketByIdQuery() : IRequest<GetTicketByIdResponse>;
