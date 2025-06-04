namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed record TicketRelationUpdateDto(Guid Id, Guid SourceId, Guid TargetId, int RelationType);