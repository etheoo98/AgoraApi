using FluentValidation;

namespace Application.Auth.Commands.LoginWithRefreshToken;

public class LoginWithRefreshTokenCommandValidator : AbstractValidator<LoginWithRefreshToken>
{
    public LoginWithRefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required");
    }
}