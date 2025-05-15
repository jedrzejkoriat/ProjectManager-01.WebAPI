namespace ProjectManager_01.WebAPI.Data
{
    public sealed class Comment
    {
        // ID
        public int Id { get; set; }
        public int TicketId { get; set; }
        // STRING
        public string Content { get; set; }
        // DATE
        public DateTime CreatedAt { get; set; }
    }
}
