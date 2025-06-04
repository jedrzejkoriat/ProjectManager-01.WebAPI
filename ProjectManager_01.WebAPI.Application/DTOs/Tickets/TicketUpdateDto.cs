namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed record TicketUpdateDto
    (Guid Id, Guid ProjectId, Guid PriorityId, Guid? AssigneeId, Guid ReporterId,
    int Status, int Resolution, int TicketType,
    string Title, string? Description, string? Version);