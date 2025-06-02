using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTagById;

public record GetTagByIdQuery() : IRequest<GetTagByIdResponse>;
