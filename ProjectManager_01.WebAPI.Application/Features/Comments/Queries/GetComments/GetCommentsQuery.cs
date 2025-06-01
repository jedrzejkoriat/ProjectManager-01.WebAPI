using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetComments;

public record GetCommentsQuery() : IRequest<GetCommentsResponse>;
