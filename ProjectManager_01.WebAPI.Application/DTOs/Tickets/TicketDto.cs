using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed record TicketDto
    (Guid Id, Guid ProjectId, Guid PriorityId, Guid AssigneeId, Guid ReporterId, 
    int Status, int Resolution, int TicketType, int TicketNumber,
    string Title, string? Description, string? Version, bool IsDeleted, DateTimeOffset CreatedAt);