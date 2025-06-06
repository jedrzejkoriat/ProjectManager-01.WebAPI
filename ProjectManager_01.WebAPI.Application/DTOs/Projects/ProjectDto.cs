namespace ProjectManager_01.Application.DTOs.Projects;

public sealed record ProjectDto(Guid Id, string Name, string Key, bool IsDeleted, DateTimeOffset CreatedAt);