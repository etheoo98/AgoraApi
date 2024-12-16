using Application.Comments.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Comments.Commands.CreateComment;

public sealed record CreateCommentCommand(
    string Content, 
    int TopicId, 
    int AuthorId) : IRequest<Result<CommentDto>>;

public class CreateComment(ITopicRepository topicRepository, ICommentFactory commentFactory, ICommentRepository commentRepository) : IRequestHandler<CreateCommentCommand, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = await topicRepository.GetTopicByIdAsync(request.TopicId, cancellationToken);
            if (thread is null || thread.Deleted is not null)
            {
                return Result<CommentDto>.NotFound("Thread does not exist");
            }
            
            var comment = commentFactory.Create(request.Content, request.TopicId, request.AuthorId);
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