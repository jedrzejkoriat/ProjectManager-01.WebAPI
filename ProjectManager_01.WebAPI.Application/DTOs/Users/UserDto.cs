using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.Users;

public sealed record UserDto (Guid Id, string UserName, string Email, bool IsDeleted, DateTimeOffset CreatedAt);