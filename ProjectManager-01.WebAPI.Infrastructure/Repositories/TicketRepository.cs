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
    public Task<Guid> CreateAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ticket>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Ticket ticket)
    {
        throw new NotImplementedException();
    }
}
