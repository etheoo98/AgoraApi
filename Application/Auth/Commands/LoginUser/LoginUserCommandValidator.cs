﻿using FluentValidation;

namespace Application.Auth.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUser>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.Username))
            .WithMessage("Either Email or Username must be provided.");
    }
}