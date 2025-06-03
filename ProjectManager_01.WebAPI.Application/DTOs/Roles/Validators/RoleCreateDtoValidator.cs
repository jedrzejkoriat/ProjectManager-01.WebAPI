using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Roles.Validators;

public sealed class RoleCreateDtoValidator : AbstractValidator<RoleCreateDto>
{
    public RoleCreateDtoValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Name cannot be longer that 20 characters.");
    }
}