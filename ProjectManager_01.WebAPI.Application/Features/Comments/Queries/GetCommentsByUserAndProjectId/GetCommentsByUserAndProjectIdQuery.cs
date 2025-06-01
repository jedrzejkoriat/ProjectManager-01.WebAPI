using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentsByUserAndProjectId
{
    public record GetCommentsByUserAndProjectIdQuery() : IRequest<GetCommentsByUserAndProjectIdResponse>;
}
