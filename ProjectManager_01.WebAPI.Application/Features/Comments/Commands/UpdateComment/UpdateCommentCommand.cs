using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Commands.UpdateComment;

public record UpdateCommentCommand() : IRequest<UpdateCommentResponse>;
