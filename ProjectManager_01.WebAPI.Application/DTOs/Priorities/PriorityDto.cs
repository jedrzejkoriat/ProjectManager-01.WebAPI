using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Priorities;

public sealed record PriorityDto (Guid Id, string Name, int Level);
