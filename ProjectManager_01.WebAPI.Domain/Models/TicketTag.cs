namespace ProjectManager_01.Domain.Models;

public sealed class TicketTag
{
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
    public Guid TicketId { get; set; }
}
