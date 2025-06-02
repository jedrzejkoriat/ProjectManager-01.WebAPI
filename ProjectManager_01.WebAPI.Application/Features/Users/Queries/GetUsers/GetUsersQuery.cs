using MediatR;

namespace ProjectManager_01.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<GetUsersResponse>;
