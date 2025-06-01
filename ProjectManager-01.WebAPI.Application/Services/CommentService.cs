using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
}
