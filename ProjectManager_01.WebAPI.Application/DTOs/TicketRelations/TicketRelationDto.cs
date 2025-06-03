using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed record TicketRelationDto (Guid Id, TicketOverviewDto Source, TicketOverviewDto Target, int RelationType);