using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Users;
public sealed record UserLoginDto(string UserName, string Password);