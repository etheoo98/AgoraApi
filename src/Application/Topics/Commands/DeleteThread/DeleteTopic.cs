using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Topics.Commands.DeleteThread;

public sealed record DeleteTopicCommand(
    int Id,
    int UserId) : IRequest<Result>, IHasId;

public class DeleteTopicHandler(ITopicRepository topicRepository) : IRequestHandler<DeleteTopicCommand, Result>
{
    public async Task<Result> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = await topicRepository.GetTopicByIdAsync(request.Id, cancellationToken);
            if (thread is null)
            {
                return Result.NotFound("Thread could not be found");
            }

            if (!UserIsTopicAuthor(thread.AuthorId, request.UserId))
            {
                return Result.Forbidden("Cannot delete thread created by another user");
            }
        
            await topicRepository.DeleteTopicAndComments(thread, cancellationToken);
            
            return Result.SuccessWithMessage("Thread deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.CriticalError("Failed to delete thread");
        }
    }

    private static bool UserIsTopicAuthor(int authorId, int userId)
    {
        return authorId == userId;
    }
}