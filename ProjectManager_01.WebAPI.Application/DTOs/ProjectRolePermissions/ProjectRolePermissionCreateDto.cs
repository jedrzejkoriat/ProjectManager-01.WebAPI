using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.ProjectRolePermissions;

public sealed class ProjectRolePermissionCreateDto
{
    public Guid ProjectRoleId { get; set; }
    public Guid PermissionId { get; set; }
}
