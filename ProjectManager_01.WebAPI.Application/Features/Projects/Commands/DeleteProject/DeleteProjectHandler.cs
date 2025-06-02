using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public DeleteProjectHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<DeleteProjectResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
