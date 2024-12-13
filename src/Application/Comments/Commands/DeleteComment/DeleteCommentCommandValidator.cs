using FluentValidation;

namespace Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(command => command.CommentId).GreaterThan(0);
    }
}