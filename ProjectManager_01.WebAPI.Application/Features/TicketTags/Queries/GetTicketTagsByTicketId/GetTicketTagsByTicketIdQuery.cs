using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagsByTicketId;

public record GetTicketTagsByTicketIdQuery() : IRequest<GetTicketTagsByTicketIdResponse>;
