namespace ProjectManager_01.WebAPI.Data;

public sealed class Comment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
