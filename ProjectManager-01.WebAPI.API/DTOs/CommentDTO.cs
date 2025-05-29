namespace ProjectManager_01.API.DTOs;

public sealed class CommentDTO
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
