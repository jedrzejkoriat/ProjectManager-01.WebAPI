using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Enums;

namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed class TicketCreateDto
{
    public Guid ProjectId { get; set; }
    public Guid PriorityId { get; set; }
    public Guid ReporterId { get; set; }
    public int Status { get; set; }
    public int Resolution { get; set; }
    public int TicketType { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
}