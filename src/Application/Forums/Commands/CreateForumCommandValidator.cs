using FluentValidation;

namespace Application.Forums.Commands;

public class CreateForumCommandValidator : AbstractValidator<CreateForumCommand>
{
    public CreateForumCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
            .MaximumLength(130).WithMessage("Name cannot be longer than 130 characters.");
        
        RuleFor(c => c.Description)
            .MaximumLength(300).WithMessage("Description cannot be longer than 300 characters.");
    }
}