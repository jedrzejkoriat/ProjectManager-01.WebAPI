using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Contracts.Auth;
public interface IUserAccessValidator
{
    Guid GetAuthenticatedUser();
    void ValidateUserIdAsync(Guid providedUserId);
}
