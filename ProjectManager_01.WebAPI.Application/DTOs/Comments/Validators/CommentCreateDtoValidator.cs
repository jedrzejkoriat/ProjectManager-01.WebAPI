using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Comments.Validators;

public sealed class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(1).WithMessage("Name must be at least 1 characters long.")
            .MaximumLength(4000).WithMessage("Name cannot be longer that 4000 characters.");

        RuleFor(c => c.TicketId)
            .Must(id => id != Guid.Empty).WithMessage("TicketId must be a valid GUID.");

        RuleFor(c => c.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}