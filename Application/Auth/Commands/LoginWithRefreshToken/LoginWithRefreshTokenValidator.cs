using FluentValidation;

namespace Application.Auth.Commands.LoginWithRefreshToken;

public class LoginWithRefreshTokenValidator : AbstractValidator<LoginWithRefreshTokenCommand>
{
    public LoginWithRefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required");
    }
}