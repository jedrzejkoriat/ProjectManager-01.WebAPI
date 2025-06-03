using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed record TicketRelationUpdateDto (Guid Id, Guid SourceId, Guid TargetId, int RelationType);