namespace ProjectManager_01.WebAPI.Domain.Models;

public sealed class TicketTag
{
    public Guid TagId { get; set; }
    public Guid TicketId { get; set; }
}
