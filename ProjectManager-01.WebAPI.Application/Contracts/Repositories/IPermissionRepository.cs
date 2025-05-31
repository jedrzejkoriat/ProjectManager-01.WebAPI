using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IPermissionRepository : 
	ICreateable<Permission>, IReadable<Permission>, IUpdateable<Permission>, IDeleteable
{
}
