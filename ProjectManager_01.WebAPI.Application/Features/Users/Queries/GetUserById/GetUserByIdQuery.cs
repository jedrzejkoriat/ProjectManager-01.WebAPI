using MediatR;

namespace ProjectManager_01.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery() : IRequest<GetUserByIdResponse>;
