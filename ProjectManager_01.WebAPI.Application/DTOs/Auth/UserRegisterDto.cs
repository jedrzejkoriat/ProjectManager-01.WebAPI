using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Auth;

public sealed record UserRegisterDto (string UserName, string Password, string Email);