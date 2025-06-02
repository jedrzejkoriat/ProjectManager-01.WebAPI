using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTagsByProjectId;

public record GetTagsByProjectIdQuery() : IRequest<GetTagsByProjectIdResponse>;
