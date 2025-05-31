using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IPriorityRepository : 
	ICreateable<Priority>, IReadable<Priority>, IUpdateable<Priority>, IDeleteable
{
}
