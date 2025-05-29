namespace ProjectManager_01.Domain.Models;

public sealed class Comment
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
