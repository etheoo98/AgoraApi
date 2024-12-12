using Application.Threads.Responses;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using Thread = Domain.Entities.Thread;

namespace Application.Threads.Commands.UpdateThread;

public sealed record UpdateThreadCommand(
    int ThreadId,
    int UserId,
    string Title, 
    string Content) : IRequest<Result<ThreadDto>>;

public class UpdateThreadHandler(IThreadRepository threadRepository) : IRequestHandler<UpdateThreadCommand, Result<ThreadDto>>
{
    public async Task<Result<ThreadDto>> Handle(UpdateThreadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = await threadRepository.GetThreadByIdAsync(request.ThreadId, cancellationToken);
            if (thread is null)
            {
                return Result<ThreadDto>.NotFound("Thread not found");
            }

            if (!IsUserAuthorized(thread.AuthorId, request.UserId))
            {
                return Result<ThreadDto>.Forbidden("Cannot update thread created by another user");
            }
            
            await threadRepository.UpdateThread(thread, request.Title, request.Content, cancellationToken);

            return MapToResponse(thread);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<ThreadDto>.CriticalError("Failed to update thread");
        }
    }

    private static bool IsUserAuthorized(int authorId, int userId)
    {
        return authorId == userId;
    }

    private static Result<ThreadDto> MapToResponse(Thread thread)
    {
        var response = thread.Adapt<ThreadDto>();
        return Result<ThreadDto>.Success(response);
    }
}