using FluentValidation;

namespace ProjectManager_01.Application.DTOs.TicketTags.Validators;

public sealed class TicketTagCreateDtoValidator : AbstractValidator<TicketTagCreateDto>
{
    public TicketTagCreateDtoValidator()
    {
        RuleFor(t => t.TagId)
            .Must(id => id != Guid.Empty).WithMessage("TagId must be a valid GUID.");

        RuleFor(t => t.TicketId)
            .Must(id => id != Guid.Empty).WithMessage("TicketId must be a valid GUID.");
    }
}