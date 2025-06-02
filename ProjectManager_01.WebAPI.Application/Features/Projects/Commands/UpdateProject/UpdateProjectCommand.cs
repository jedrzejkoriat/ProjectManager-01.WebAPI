using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Commands.UpdateProject;

public record UpdateProjectCommand() : IRequest<UpdateProjectResponse>;
