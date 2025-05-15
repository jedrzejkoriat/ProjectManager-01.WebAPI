namespace ProjectManager_01.WebAPI.Data
{
    internal sealed class Ticket
    {
        // ID
        public int Id { get; set; }
        public int ProjectId { get; init; }
        public int? TagId { get; init; }
        public int TypeId { get; init; }
        public int PriorityId { get; init; }
        public int StatusId { get; init; }
        public int? AssigneeId { get; init; }
        public int ReporterId { get; init; }
        public int ResolutionId { get; init; }
        // STRING
        public string Title { get; init; }
        public string? Description { get; init; }
        public string? Version { get; init; }
        // DATE
        public DateTime CreatedAt { get; init; }
    }
}
