using ProjectManager_01.WebAPI.Application.Contracts;
using ProjectManager_01.WebAPI.Application.DTOs;

namespace ProjectManager_01.WebAPI.Infrastructure.Repositories;
public class ProjectsRepository : IProjectsRepository
{
    public ProjectDTO GetProjectDTO(ProjectDTO projectDTO)
    {
        return new ProjectDTO { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Name = "Sample Project" };
    }
}
