using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Commands.DeleteComment;

public record DeleteCommentCommand() : IRequest<DeleteCommentResponse>;
