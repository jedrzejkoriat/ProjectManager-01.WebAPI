using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.DTOs.Comments;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Application.DTOs.Projects;
using ProjectManager_01.Application.DTOs.Tags;
using ProjectManager_01.Application.DTOs.TicketRelations;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Application.DTOs.Tickets;
public sealed record TicketOverviewDto (Guid Id, ProjectDto Project, string Status, string TicketType, int TicketNumber, string Title);