using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.ProjectRoles;

public sealed record ProjectRoleDto (Guid Id, Guid ProjectId, string Name, IEnumerable<PermissionDto> Permissions);
