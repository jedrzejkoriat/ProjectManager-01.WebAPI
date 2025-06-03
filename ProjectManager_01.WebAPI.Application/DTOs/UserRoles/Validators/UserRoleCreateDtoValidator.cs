using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.UserRoles.Validators;

public sealed class UserRoleCreateDtoValidator : AbstractValidator<UserRoleCreateDto>
{
    public UserRoleCreateDtoValidator()
    {
        RuleFor(u => u.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}