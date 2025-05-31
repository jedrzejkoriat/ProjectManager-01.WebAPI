using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface ITicketRepository : ICreateable<Ticket>, IReadable<Ticket>, IUpdateable<Ticket>, IDeleteable, ISoftDeletable
{
    Task<List<Ticket>> GetByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByKeyAndNumberAsync(string key, int number);
    Task<Ticket> GetByIdAsync(Guid id);
}
