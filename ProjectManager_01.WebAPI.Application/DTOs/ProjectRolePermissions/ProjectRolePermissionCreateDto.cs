using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.ProjectRolePermissions;

public sealed record ProjectRolePermissionCreateDto (Guid ProjectRoleId, Guid PermissionId);
