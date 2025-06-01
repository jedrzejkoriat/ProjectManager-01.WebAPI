using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Features.Shared;
public record UserRecord(Guid Id, string UserName, string Email, bool IsDeleted);
