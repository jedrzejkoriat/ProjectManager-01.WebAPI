namespace ProjectManager_01.Application.DTOs.ProjectRoles;

public sealed record ProjectRoleUpdateDto(Guid Id, Guid ProjectId, string Name);