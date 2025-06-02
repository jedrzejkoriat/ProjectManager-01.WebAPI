using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTags;

public record GetTagsQuery() : IRequest<GetTagsResponse>;
