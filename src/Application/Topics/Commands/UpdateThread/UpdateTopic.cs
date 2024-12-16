using Application.Topics.Responses;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Topics.Commands.UpdateThread;

public sealed record UpdateTopicCommand(
    int TopicId,
    int UserId,
    string Title, 
    string Content) : IRequest<Result<TopicDto>>;

public class UpdateTopicHandler(ITopicRepository topicRepository) : IRequestHandler<UpdateTopicCommand, Result<TopicDto>>
{
    public async Task<Result<TopicDto>> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var thread = await topicRepository.GetTopicByIdAsync(request.TopicId, cancellationToken);
            if (thread is null)
            {
                return Result<TopicDto>.NotFound("Thread not found");
            }

            if (!IsUserAuthorized(thread.AuthorId, request.UserId))
            {
                return Result<TopicDto>.Forbidden("Cannot update thread created by another user");
            }
            
            await topicRepository.UpdateTopic(thread, request.Title, request.Content, cancellationToken);

            return MapToResponse(thread);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TopicDto>.CriticalError("Failed to update thread");
        }
    }

    private static bool IsUserAuthorized(int authorId, int userId)
    {
        return authorId == userId;
    }

    private static Result<TopicDto> MapToResponse(Topic topic)
    {
        var response = topic.Adapt<TopicDto>();
        return Result<TopicDto>.Success(response);
    }
}