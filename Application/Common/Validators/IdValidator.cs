using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Common.Validators;

public abstract class IdValidator<T> : AbstractValidator<T> where T : IHasId
{
    protected IdValidator()
    {
        RuleFor(T => T.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}