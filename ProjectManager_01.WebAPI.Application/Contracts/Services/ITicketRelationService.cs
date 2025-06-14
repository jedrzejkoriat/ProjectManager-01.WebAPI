﻿using System.Data;
using ProjectManager_01.Application.DTOs.TicketRelations;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketRelationService
{
    Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationCreateDto, Guid projectId);
    Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationUpdateDto, Guid projectId);
    Task DeleteTicketRelationAsync(Guid ticketRelationId, Guid projectId);
    Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid ticketRelationId, Guid projectId);
    Task<IEnumerable<TicketRelationDto>> GetAllTicketRelationsAsync();
    Task DeleteTicketRelationByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
    Task<IEnumerable<TicketRelationDto>> GetTicketRelationsBySourceIdAsync(Guid ticketId);
    Task<IEnumerable<TicketRelationDto>> GetTicketRelationsByTargetIdAsync(Guid ticketId);
}
