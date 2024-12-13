using FluentValidation;

namespace Application.Threads.Commands.UpdateThread;

public class UpdateThreadValidator : AbstractValidator<UpdateThreadCommand>
{
    public UpdateThreadValidator()
    {
        RuleFor(x => x.ThreadId)
            .GreaterThan(0).WithMessage("Thread Id must be greater than 0.");
        
        RuleFor(x => new { x.Title, x.Content})
            .Must(properties => 
                !string.IsNullOrEmpty(properties.Title) ||
                !string.IsNullOrEmpty(properties.Content)
            ).WithMessage("At least one of the properties (Title or Content) must be provided.");
        
        RuleFor(c => c.Title)
            .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
            .MaximumLength(130).WithMessage("Name cannot be longer than 130 characters.");
        
        RuleFor(c => c.Content)
            .MinimumLength(10).WithMessage("Content must have at least 10 characters.")
            .MaximumLength(5000).WithMessage("Content cannot be longer than 5000 characters.");
    }
}