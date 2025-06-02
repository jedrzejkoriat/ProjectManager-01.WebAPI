using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.TicketTags;

public sealed class TicketTagDto
{
    public Guid TagId { get; set; }
    public Guid TicketId { get; set; }
}