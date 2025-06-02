using MediatR;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagById;

public record GetTicketTagByIdQuery() : IRequest<GetTicketTagByIdResponse>;
