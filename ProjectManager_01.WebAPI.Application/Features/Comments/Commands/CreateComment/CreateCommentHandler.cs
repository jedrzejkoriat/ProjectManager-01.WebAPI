using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CreateCommentResponse>
{
    private readonly ICommentRepository commentRepository;
    private readonly ICommentService commentService;

    public CreateCommentHandler(ICommentRepository commentRepository, ICommentService commentService)
    {
        this.commentRepository = commentRepository;
        this.commentService = commentService;
    }

    public async Task<CreateCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
