using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTags;

public record GetTicketTagsQuery() : IRequest<GetTicketTagsResponse>;
