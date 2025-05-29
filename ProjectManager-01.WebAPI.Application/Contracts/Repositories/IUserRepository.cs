using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task<Guid> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
}
