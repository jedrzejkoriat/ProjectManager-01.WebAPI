using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IProjectRepository : ICreateable<Project>, IUpdateable<Project>, ISoftDeletable
{
    Task<Project> GetByIdAsync(Guid id);
}
