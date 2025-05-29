using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TicketRepository : ITicketRepository
{
    public Task<Guid> CreateAsync(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetByKeyAndNumberAsync(string key, int number)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ticket>> GetByProjectIdAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Ticket entity)
    {
        throw new NotImplementedException();
    }
}
