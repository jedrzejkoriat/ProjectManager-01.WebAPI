using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public class TicketRelation
{
    public Guid TargetId { get; set; }
    public Guid SourceId { get; set; }
    public RelationType RelationType { get; set; }
}
