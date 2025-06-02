namespace ProjectManager_01.Application.DTOs;

public sealed class TicketRelationDTO
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
    public int RelationType { get; set; }
}
