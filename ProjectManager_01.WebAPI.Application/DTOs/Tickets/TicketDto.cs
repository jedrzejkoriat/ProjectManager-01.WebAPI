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
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.DTOs.Tickets;

public sealed record TicketDto
    (Guid Id, ProjectDto Project, PriorityDto Priority, UserDto Assignee, UserDto Reporter, 
    string Status, string Resolution, string TicketType, int TicketNumber,
    string Title, string? Description, string? Version, bool IsDeleted, DateTimeOffset CreatedAt,
    IEnumerable<TagDto> Tags, IEnumerable<CommentDto> Comments, 
    IEnumerable<TicketRelationDto> RelationsAsSource, IEnumerable<TicketRelationDto> RelationsAsTarget);