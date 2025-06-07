using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Users.Validators;

public sealed class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.")
            .MinimumLength(5).WithMessage("UserName must be at least 5 characters long.")
            .MaximumLength(50).WithMessage("UserName cannot be longer that 50 characters.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(100).WithMessage("Email cannot be longer that 100 characters.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password needs to contain a capital letter.")
            .Matches("[a-z]").WithMessage("Password needs to contain a small letter.")
            .Matches("[0-9]").WithMessage("Password needs to contain a numer.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password needs to contain a symbol.");
    }
}