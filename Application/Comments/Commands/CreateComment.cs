using Application.Comments.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Comments.Commands;

public sealed record CreateCommentCommand(
    string Content, 
    int ThreadId, 
    int AuthorId) : IRequest<Result<CommentDto>>;

public class CreateComment(IThreadRepository threadRepository, ICommentFactory commentFactory, ICommentRepository commentRepository) : IRequestHandler<CreateCommentCommand, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var threadExists = await threadRepository.ThreadExists(request.ThreadId, cancellationToken);
            if (!threadExists) // TODO: Verify that the thread isn't soft deleted before adding comment.
            {
                return Result<CommentDto>.NotFound("Thread does not exist");
            }
            
            var comment = commentFactory.Create(request.Content, request.ThreadId, request.AuthorId);
            await commentRepository.AddCommentAsync(comment, cancellationToken);

            var response = comment.Adapt<CommentDto>();
            return Result<CommentDto>.Success(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<CommentDto>.CriticalError("Failed to create comment");
        }
    }
}