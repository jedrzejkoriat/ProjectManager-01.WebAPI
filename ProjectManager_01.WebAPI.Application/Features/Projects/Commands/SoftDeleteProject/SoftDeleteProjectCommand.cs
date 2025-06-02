using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Commands.SoftDeleteProject;

public record SoftDeleteProjectCommand() : IRequest<SoftDeleteProjectResponse>;
