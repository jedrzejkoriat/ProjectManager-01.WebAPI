using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Tags.Validators;

public sealed class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
{
    public TagUpdateDtoValidator()
    {
        RuleFor(t => t.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(t => t.ProjectId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectId must be a valid GUID.");

        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 character long.")
            .MaximumLength(20).WithMessage("Name cannot be longer that 20 characters.");
    }
}