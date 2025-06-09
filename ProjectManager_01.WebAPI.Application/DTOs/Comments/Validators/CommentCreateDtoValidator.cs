using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Comments.Validators;

public sealed class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Content cannot be empty.")
            .MinimumLength(1).WithMessage("Content must be at least 1 characters long.")
            .MaximumLength(4000).WithMessage("Content cannot be longer that 4000 characters.");

        RuleFor(c => c.TicketId)
            .Must(id => id != Guid.Empty).WithMessage("TicketId must be a valid GUID.");
    }
}