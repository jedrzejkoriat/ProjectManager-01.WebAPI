using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Tickets.Validators;

public sealed class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
{
    public TicketCreateDtoValidator()
    {
        RuleFor(t => t.ProjectId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectId must be a valid GUID.");

        RuleFor(t => t.PriorityId)
            .Must(id => id != Guid.Empty).WithMessage("PriorityId must be a valid GUID.");

        RuleFor(t => t.ReporterId)
            .Must(id => id != Guid.Empty).WithMessage("ReporterId must be a valid GUID.");

        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(200).WithMessage("Title cannot be longer that 200 characters.");
    }
}