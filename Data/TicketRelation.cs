using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public sealed class TicketRelation
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
    public RelationType RelationType { get; set; }
}
