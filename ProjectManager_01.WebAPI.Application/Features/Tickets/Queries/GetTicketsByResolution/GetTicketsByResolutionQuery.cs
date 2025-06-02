using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByResolution;

public record GetTicketsByResolutionQuery() : IRequest<GetTicketsByResolutionResponse>;
