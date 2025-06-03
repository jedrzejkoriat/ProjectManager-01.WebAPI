using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Tags;

public sealed record TagDto (Guid Id, Guid ProjectId, string Name);