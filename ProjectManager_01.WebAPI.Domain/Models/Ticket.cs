using ProjectManager_01.Domain.Enums;

namespace ProjectManager_01.Domain.Models;

public sealed class Ticket
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public Guid PriorityId { get; set; }
    public Priority Priority { get; set; }
    public Guid? AssigneeId { get; set; }
    public User? Assignee { get; set; }
    public Guid ReporterId { get; set; }
    public User Reporter { get; set; }
    public Status Status { get; set; }
    public Resolution Resolution { get; set; }
    public TicketType TicketType { get; set; }
    public int TicketNumber { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }
    public List<TicketRelation> RelationsAsSource { get; set; }
    public List<TicketRelation> RelationsAsTarget { get; set; }
}