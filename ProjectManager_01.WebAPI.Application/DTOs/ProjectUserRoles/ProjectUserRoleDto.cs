using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.ProjectUserRoles;

public sealed class ProjectUserRoleDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ProjectRoleId { get; set; }
    public Guid UserId { get; set; }
}
