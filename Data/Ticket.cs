using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public sealed class Ticket
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int PriorityId { get; set; }
    public int? AssigneeId { get; set; }
    public int ReporterId { get; set; }
    public Status Status { get; set; }
    public Resolution Resolution { get; set; }
    public TicketType TicketType { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}