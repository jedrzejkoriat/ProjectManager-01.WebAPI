namespace ProjectManager_01.Application.DTOs.ProjectUserRoles;

public sealed record ProjectUserRoleDto(Guid Id, Guid ProjectId, Guid ProjectRoleId, Guid UserId);