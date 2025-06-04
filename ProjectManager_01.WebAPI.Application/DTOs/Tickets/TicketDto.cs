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

public sealed class TicketDto
{
    public Guid Id { get; init; }
    public ProjectDto Project { get; init; }
    public PriorityDto Priority { get; init; }
    public UserDto Assignee { get; init; }
    public UserDto Reporter { get; init; }
    public string Status { get; init; }
    public string Resolution { get; init; }
    public string TicketType { get; init; }
    public int TicketNumber { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string? Version { get; init; }
    public bool IsDeleted { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public IEnumerable<TagDto> Tags { get; set; }
    public IEnumerable<CommentDto> Comments { get; set; }
    public IEnumerable<TicketRelationDto> RelationsAsSource { get; set; }
    public IEnumerable<TicketRelationDto> RelationsAsTarget { get; set; }
}