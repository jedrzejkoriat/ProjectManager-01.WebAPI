using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public sealed class Ticket
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid PriorityId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid? ReporterId { get; set; }
    public Status Status { get; set; }
    public Resolution Resolution { get; set; }
    public TicketType TicketType { get; set; }
    public int TicketNumber { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}