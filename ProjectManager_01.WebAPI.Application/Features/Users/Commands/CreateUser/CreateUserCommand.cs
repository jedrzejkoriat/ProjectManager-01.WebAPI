using MediatR;

namespace ProjectManager_01.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand() : IRequest<CreateUserResponse>;
