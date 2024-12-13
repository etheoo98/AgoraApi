using Ardalis.Result;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(int UserId, int CommentId) : IRequest<Result>;

public class DeleteCommentHandler(ICommentRepository commentRepository) : IRequestHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await commentRepository.GetCommentByIdAsync(request.CommentId, cancellationToken);
            if (comment is null)
            {
                return Result.NotFound("Comment not found");
            }

            if (!UserIsAuthor(request.UserId, comment.AuthorId))
            {
                return Result.Forbidden("Cannot delete comment made by another user");
            }
            
            await commentRepository.DeleteCommentAsync(comment, cancellationToken);
            
            return Result.SuccessWithMessage("Comment deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.CriticalError("");
        }
    }

    private bool UserIsAuthor(int userId, int authorId)
    {
        return userId == authorId;
    }
}