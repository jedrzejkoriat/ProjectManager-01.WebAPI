namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed record TicketCreateDto
    (Guid ProjectId, Guid PriorityId,
    int Status, int Resolution, int TicketType,
    string Title, string? Description, string? Version,
    IEnumerable<Guid> TagIds);