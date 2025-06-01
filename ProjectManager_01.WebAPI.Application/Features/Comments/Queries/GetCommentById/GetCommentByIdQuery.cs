using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentById;

public record GetCommentByIdQuery() : IRequest<GetCommentByIdResponse>;
