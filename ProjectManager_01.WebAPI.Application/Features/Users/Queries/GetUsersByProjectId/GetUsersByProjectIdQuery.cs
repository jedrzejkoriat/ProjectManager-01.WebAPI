using MediatR;

namespace ProjectManager_01.Application.Features.Users.Queries.GetUsersByProjectId;

public record GetUsersByProjectIdQuery() : IRequest<GetUsersByProjectIdResponse>;
