using ProjectManager_01.Application.DTOs.Projects;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IProjectService
{
    Task CreateProjectAsync(ProjectCreateDto projectCreateDto);
    Task UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
    Task DeleteProjectAsync(Guid projectId);
    Task<ProjectDto> GetProjectByIdAsync(Guid projectId);
    Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    Task SoftDeleteProjectAsync(Guid projectId);
    Task<IEnumerable<ProjectDto>> GetProjectsByUserIdAsync(Guid userId);
}
