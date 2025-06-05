using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Application.Contracts.Authorization;

public interface IJwtGenerator
{
    string GenerateToken(UserClaimsDto userClaimsDto);
}
