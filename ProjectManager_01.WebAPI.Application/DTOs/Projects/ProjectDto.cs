using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.Projects;

public sealed class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
