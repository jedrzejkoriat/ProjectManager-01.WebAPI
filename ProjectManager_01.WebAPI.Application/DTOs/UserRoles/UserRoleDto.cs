using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.UserRoles;

public sealed record UserRoleDto (Guid UserId, Guid RoleId);