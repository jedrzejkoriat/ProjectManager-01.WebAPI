namespace ProjectManager_01.WebAPI.Application.DTOs;

public sealed class TicketDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid PriorityId { get; set; }
    public Guid? AssigneeId { get; set; }
    public Guid ReporterId { get; set; }
    public int Status { get; set; }
    public int Resolution { get; set; }
    public int TicketType { get; set; }
    public int TicketNumber { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}