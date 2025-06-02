using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Comments.Commands.UpdateComment;

public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentResponse>
{
    private readonly ICommentRepository commentRepository;
    private readonly ICommentService commentService;

    public UpdateCommentHandler(ICommentRepository commentRepository, ICommentService commentService)
    {
        this.commentRepository = commentRepository;
        this.commentService = commentService;
    }

    public async Task<UpdateCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
