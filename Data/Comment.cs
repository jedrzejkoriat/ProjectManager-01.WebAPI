namespace ProjectManager_01.WebAPI.Data
{
    internal sealed class Comment
    {
        // ID
        public int Id { get; set; }
        public int TicketId { get; init; }
        // STRING
        public string Content { get; init; }
        // DATE
        public DateTime CreatedAt { get; init; }
    }
}
