namespace ProjectManager_01.Application.DTOs.ProjectUserRoles;

public sealed record ProjectUserRoleCreateDto(Guid ProjectId, Guid ProjectRoleId, Guid UserId);