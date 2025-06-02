using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Commands.CreateComment;

public record CreateCommentCommand() : IRequest<CreateCommentResponse>;
