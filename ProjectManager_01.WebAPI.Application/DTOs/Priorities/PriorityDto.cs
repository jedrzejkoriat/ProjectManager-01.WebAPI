using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Priorities;

public sealed class PriorityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
}
