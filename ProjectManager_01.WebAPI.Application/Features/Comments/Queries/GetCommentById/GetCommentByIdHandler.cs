using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentById;

public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, GetCommentByIdResponse>
{
    private readonly ICommentRepository commentRepository;
    private readonly ICommentService commentService;

    public GetCommentByIdHandler(ICommentRepository commentRepository, ICommentService commentService)
    {
        this.commentRepository = commentRepository;
        this.commentService = commentService;
    }

    public async Task<GetCommentByIdResponse> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
