using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class CommentRepository : ICommentRepository
{
    public Task<Guid> CreateAsync(Comment entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Comment>> GetByTicketIdAsync(Guid ticketId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Comment entity)
    {
        throw new NotImplementedException();
    }
}
