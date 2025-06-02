using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.UserRoles;

public sealed class UserRoleUpdateDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}