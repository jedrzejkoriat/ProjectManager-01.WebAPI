using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Enums;

namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed class TicketRelationCreateDto
{
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
    public int RelationType { get; set; }
}