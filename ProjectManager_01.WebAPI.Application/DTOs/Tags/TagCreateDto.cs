namespace ProjectManager_01.Application.DTOs.Tags;

public sealed record TagCreateDto(Guid ProjectId, string Name);