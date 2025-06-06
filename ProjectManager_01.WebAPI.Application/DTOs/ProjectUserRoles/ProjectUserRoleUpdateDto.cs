namespace ProjectManager_01.Application.DTOs.ProjectUserRoles;

public sealed record ProjectUserRoleUpdateDto(Guid Id, Guid ProjectId, Guid ProjectRoleId, Guid UserId);
