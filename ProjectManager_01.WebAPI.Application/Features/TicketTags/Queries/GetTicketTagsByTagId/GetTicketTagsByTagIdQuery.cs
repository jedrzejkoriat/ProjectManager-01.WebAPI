using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagsByTagId;

public record GetTicketTagsByTagIdQuery() : IRequest<GetTicketTagsByTagIdResponse>;
