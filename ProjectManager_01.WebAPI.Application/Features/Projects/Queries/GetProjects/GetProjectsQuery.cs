using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Queries.GetProjects;

public record GetProjectsQuery() : IRequest<GetProjectsResponse>;
