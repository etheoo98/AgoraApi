using FluentValidation;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(user => user.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        
        RuleFor(user => new { user.Email, user.Username, user.Password })
            .Must(properties => 
                !string.IsNullOrEmpty(properties.Email) ||
                !string.IsNullOrEmpty(properties.Username) ||
                !string.IsNullOrEmpty(properties.Password)
            )
            .WithMessage("At least one of the fields (Email, Username, or Password) must be provided.");

        RuleFor(user => user.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(user => !string.IsNullOrEmpty(user.Email));

        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(2, 20).WithMessage("Username must be between 2 and 20 characters.")
            .When(user => !string.IsNullOrEmpty(user.Username));

        RuleFor(user => user.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .When(user => !string.IsNullOrEmpty(user.Password));
    }
}