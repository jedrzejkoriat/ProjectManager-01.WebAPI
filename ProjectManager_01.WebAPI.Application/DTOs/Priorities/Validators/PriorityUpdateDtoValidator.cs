using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Priorities.Validators;

public sealed class PriorityUpdateDtoValidator : AbstractValidator<PriorityUpdateDto>
{
    public PriorityUpdateDtoValidator()
    {
        RuleFor(p => p.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Name cannot be longer that 20 characters.");

        RuleFor(p => p.Level)
            .GreaterThanOrEqualTo(0).WithMessage("Level must be greater than or equal to 0.");
    }
}