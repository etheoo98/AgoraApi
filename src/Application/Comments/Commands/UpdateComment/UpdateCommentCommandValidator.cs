using FluentValidation;

namespace Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.CommentId).GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}