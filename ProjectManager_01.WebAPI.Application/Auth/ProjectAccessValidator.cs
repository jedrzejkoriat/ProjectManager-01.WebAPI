using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Auth;

// This is helper class that validates if the projectId provided in the route matches with the projectId of the entities provided with it
public sealed class ProjectAccessValidator : IProjectAccessValidator
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITagRepository _tagRepository;

    public ProjectAccessValidator(
        ITicketRepository ticketRepository,
        ITagRepository tagRepository)
    {
        _ticketRepository = ticketRepository;
        _tagRepository = tagRepository;
    }

    // This method validates if the comment belongs to the projectId provided through the route
    public async Task ValidateTicketProjectIdAsync(Guid ticketId, Guid projectId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);
        ValidateProjectIds(ticket.Project.Id, projectId);
    }

    // Find projectId of entity by tagId
    public async Task ValidateTagProjectIdAsync(Guid tagId, Guid projectId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        ValidateProjectIds(tag.ProjectId, projectId);
    }

    // Validate projectIds
    public void ValidateProjectIds(Guid projectId, Guid providedProjectId)
    {
        if (projectId != providedProjectId)
            throw new ForbiddenException("Route projectId and projectId related to provided entity do not match.");
    }
}