using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Users.Validators;
public sealed class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password cannot be empty.");
    }
}