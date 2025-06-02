using ProjectManager_01.Application.DTOs.Comments;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CommentCreateDto commentCreateDto);
    Task<List<CommentDto>> GetCommentsAsync();
    Task<CommentDto> GetCommentAsync(Guid commentId);
    Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto);
    Task DeleteCommentAsync(Guid commentId);
}
