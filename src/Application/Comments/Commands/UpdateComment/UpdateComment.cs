using Application.Comments.Responses;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Comments.Commands.UpdateComment;

public sealed record UpdateCommentCommand(int CommentId, int UserId, string Content) : IRequest<Result<CommentDto>>;

public class UpdateCommentHandler(ICommentRepository commentRepository) : IRequestHandler<UpdateCommentCommand, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await commentRepository.GetCommentByIdAsync(request.CommentId, cancellationToken);
            if (comment is null || comment.Deleted is not null)
            {
                return Result.NotFound("Comment not found or has been deleted");
            }
            
            if (!UserIsCommentAuthor(comment.AuthorId, request.UserId))
            {
                return Result.Forbidden("Cannot update comment made by another user");
            }
            
            await commentRepository.UpdateCommentAsync(comment, request.Content, cancellationToken);

            var response = comment.Adapt<CommentDto>();
            return Result<CommentDto>.Success(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<CommentDto>.CriticalError("An error occured while updating the comment");
        }
    }

    private static bool UserIsCommentAuthor(int authorId, int userId)
    {
        return userId == authorId;
    }
}