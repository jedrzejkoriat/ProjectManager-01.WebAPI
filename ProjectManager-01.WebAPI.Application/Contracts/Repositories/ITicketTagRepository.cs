using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface ITicketTagRepository : ICreateable<TicketTag>, IReadable<TicketTag>, IUpdateable<TicketTag>, IDeleteable
{
    Task<List<TicketTag>> GetByTicketIdAsync(Guid ticketId);
    Task<List<TicketTag>> GetByTagIdAsync(Guid tagId);
}
