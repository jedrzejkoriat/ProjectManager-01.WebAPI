using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Permissions.Validators;
public sealed class PermissionCreateDtoValidator : AbstractValidator<PermissionCreateDto>
{
    public PermissionCreateDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(30).WithMessage("Name cannot be longer that 30 characters.");
    }
}