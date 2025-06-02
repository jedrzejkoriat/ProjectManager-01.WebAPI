namespace ProjectManager_01.Application.DTOs.Comments;

public sealed class CommentCreateDto
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
}
