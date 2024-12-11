using Application.Threads.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Threads.Commands.CreateThread;

public sealed record CreateThreadCommand(int CreatorUserId, string Title, string Content) : IRequest<Result<ThreadDto>>;

public class CreateThreadHandler(IThreadFactory threadFactory, IThreadRepository threadRepository) : IRequestHandler<CreateThreadCommand, Result<ThreadDto>>
{
    public async Task<Result<ThreadDto>> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = threadFactory.Create(request.Title, request.Content, request.CreatorUserId);
            await threadRepository.AddThread(thread);
            var response = thread.Adapt<ThreadDto>();
            return Result<ThreadDto>.Created(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            const string message = "An error occurred while creating thread";
            return Result<ThreadDto>.CriticalError(message);
        }
    }
}