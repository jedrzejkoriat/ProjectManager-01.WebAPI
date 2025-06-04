using System.Data;
using ProjectManager_01.Application.DTOs.Comments;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CommentCreateDto commentCreateDto);
    Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
    Task<CommentDto> GetCommentAsync(Guid commentId);
    Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto);
    Task DeleteCommentAsync(Guid commentId);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
    Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId);
}
