namespace ProjectManager_01.Application.DTOs.Tags;

public sealed record TagDto(Guid Id, Guid ProjectId, string Name);