using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Priorities.Validators;
public sealed class PriorityUpdateDtoValidator : AbstractValidator<PriorityUpdateDto>
{
    public PriorityUpdateDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(20).WithMessage("Name cannot be longer that 20 characters.");
    }
}