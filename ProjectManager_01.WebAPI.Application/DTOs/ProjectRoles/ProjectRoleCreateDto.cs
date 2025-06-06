namespace ProjectManager_01.Application.DTOs.ProjectRoles;

public sealed record ProjectRoleCreateDto(Guid ProjectRoleId, string Name, IEnumerable<Guid> PermissionIds);
