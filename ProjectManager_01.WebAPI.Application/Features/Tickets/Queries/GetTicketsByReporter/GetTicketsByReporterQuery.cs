using MediatR;

namespace ProjectManager_01.Application.Features.Tickets.Queries.GetTicketsByReporter;

public record GetTicketsByReporterQuery() : IRequest<GetTicketsByReporterResponse>;
