using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;
using ProjectManager_01.Application.Contracts.Repositories.Base;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IPriorityRepository : ICreateable<Priority>, IReadable<Priority>, IUpdateable<Priority>, IDeleteable
{
    Task<List<Priority>> GetAllAsync();
}
