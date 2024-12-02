﻿using FluentValidation;

namespace Application.Users.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(query => query.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
    }
}