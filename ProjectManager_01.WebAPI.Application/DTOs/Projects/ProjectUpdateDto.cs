using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Projects;

public sealed class ProjectUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
}
