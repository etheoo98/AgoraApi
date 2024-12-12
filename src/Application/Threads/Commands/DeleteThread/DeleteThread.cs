using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Threads.Commands.DeleteThread;

public sealed record DeleteThreadCommand(
    int Id,
    int UserId) : IRequest<Result>, IHasId;

public class DeleteThreadHandler(IThreadRepository threadRepository) : IRequestHandler<DeleteThreadCommand, Result>
{
    public async Task<Result> Handle(DeleteThreadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = await threadRepository.GetThreadByIdAsync(request.Id, cancellationToken);
            if (thread is null)
            {
                return Result.NotFound("Thread could not be found");
            }

            if (!UserIsThreadAuthor(thread.AuthorId, request.UserId))
            {
                return Result.Forbidden("Cannot delete thread created by another user");
            }
        
            await threadRepository.DeleteThread(thread, cancellationToken);
            
            return Result.SuccessWithMessage("Thread deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.CriticalError("Failed to delete thread");
        }
    }

    private static bool UserIsThreadAuthor(int authorId, int userId)
    {
        return authorId == userId;
    }
}