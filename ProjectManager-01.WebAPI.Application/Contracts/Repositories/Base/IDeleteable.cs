using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface IDeleteable
{
    Task<bool> DeleteAsync(Guid id);
}
