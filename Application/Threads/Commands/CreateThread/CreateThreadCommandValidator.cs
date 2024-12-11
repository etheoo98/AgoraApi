using FluentValidation;

namespace Application.Threads.Commands.CreateThread;

public class CreateThreadCommandValidator : AbstractValidator<CreateThreadCommand>
{
    public CreateThreadCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
            .MaximumLength(130).WithMessage("Name cannot be longer than 130 characters.");
        
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MinimumLength(10).WithMessage("Content must have at least 10 characters.")
            .MaximumLength(5000).WithMessage("Content cannot be longer than 5000 characters.");
    }
}