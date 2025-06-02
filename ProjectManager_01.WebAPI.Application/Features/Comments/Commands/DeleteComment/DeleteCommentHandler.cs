using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, DeleteCommentResponse>
{
    private readonly ICommentRepository commentRepository;
    private readonly ICommentService commentService;

    public DeleteCommentHandler(ICommentRepository commentRepository, ICommentService commentService)
    {
        this.commentRepository = commentRepository;
        this.commentService = commentService;
    }

    public async Task<DeleteCommentResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
