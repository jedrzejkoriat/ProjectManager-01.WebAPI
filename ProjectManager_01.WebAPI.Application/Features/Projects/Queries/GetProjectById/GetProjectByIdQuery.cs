using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery() : IRequest<GetProjectByIdResponse>;
