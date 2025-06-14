﻿using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketTagRepository
{
    Task<IEnumerable<TicketTag>> GetAllAsync();
    Task<TicketTag> GetByIdAsync(Guid ticketId, Guid tagId);
    Task<bool> DeleteByIdAsync(Guid ticketId, Guid tagId);
    Task<bool> CreateAsync(TicketTag ticketTag);
    Task<bool> CreateAsync(TicketTag ticketTag, IDbTransaction dbTransaction);
}
