using System.Data;
using ProjectManager_01.Application.DTOs.Comments;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CommentCreateDto commentCreateDto, Guid projectId);
    Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
    Task<CommentDto> GetCommentAsync(Guid commentId, Guid projectId);
    Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto, Guid projectId);
    Task DeleteCommentAsync(Guid commentId, Guid projectId);
    Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
    Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId);
}
