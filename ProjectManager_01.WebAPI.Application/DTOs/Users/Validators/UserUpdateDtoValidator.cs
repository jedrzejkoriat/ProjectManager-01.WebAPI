using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Users.Validators;

public sealed class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(u => u.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty.")
            .MinimumLength(5).WithMessage("UserName must be at least 5 characters long.")
            .MaximumLength(50).WithMessage("UserName cannot be longer that 50 characters.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(100).WithMessage("UserName cannot be longer that 50 characters.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}