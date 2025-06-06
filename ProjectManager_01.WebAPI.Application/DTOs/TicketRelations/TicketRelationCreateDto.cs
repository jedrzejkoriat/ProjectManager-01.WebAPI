namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed record TicketRelationCreateDto(Guid SourceId, Guid TargetId, int RelationType);