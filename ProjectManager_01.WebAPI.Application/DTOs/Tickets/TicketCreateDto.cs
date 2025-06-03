using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Enums;

namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed record TicketCreateDto
    (Guid ProjectId, Guid PriorityId, Guid ReporterId, 
    int Status, int Resolution, int TicketType, 
    string Title, string? Description, string? Version,
    IEnumerable<Guid> TagIds);