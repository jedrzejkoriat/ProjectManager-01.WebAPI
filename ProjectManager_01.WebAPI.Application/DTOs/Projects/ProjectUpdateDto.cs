using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Projects;

public sealed record ProjectUpdateDto (Guid Id, string Name, string Key);
