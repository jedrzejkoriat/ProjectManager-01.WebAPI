using MediatR;

namespace ProjectManager_01.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand() : IRequest<DeleteUserResponse>;
