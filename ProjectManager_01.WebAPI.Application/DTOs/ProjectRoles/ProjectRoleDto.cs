using ProjectManager_01.Application.DTOs.Permissions;

namespace ProjectManager_01.Application.DTOs.ProjectRoles;

public sealed record ProjectRoleDto(Guid Id, Guid ProjectId, string Name, IEnumerable<PermissionDto> Permissions);
