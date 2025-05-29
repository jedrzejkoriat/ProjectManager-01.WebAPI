using ProjectManager_01.Domain.Enums;

namespace ProjectManager_01.Domain.Models;

public sealed class TicketRelation
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public Ticket Source { get; set; }
    public Guid TargetId { get; set; }
    public Ticket Target { get; set; }
    public RelationType RelationType { get; set; }
}
