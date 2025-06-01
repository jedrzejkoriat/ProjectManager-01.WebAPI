using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Contracts.Authorization;
public interface IBcryptPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
