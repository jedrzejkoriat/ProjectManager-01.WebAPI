namespace ProjectManager_01.Application.DTOs.Tags;

public sealed record TagUpdateDto(Guid Id, Guid ProjectId, string Name);