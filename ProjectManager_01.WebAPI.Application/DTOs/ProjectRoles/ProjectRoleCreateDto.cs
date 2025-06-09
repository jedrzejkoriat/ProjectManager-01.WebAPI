namespace ProjectManager_01.Application.DTOs.ProjectRoles;

public sealed record ProjectRoleCreateDto(Guid ProjectId, string Name, IEnumerable<Guid> PermissionIds);
