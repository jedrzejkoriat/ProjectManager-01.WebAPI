using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.ProjectUserRoles;

public sealed record ProjectUserRoleCreateDto (Guid ProjectId, Guid ProjectRoleId, Guid UserId);