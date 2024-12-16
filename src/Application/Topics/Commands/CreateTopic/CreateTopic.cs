using Application.Topics.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Topics.Commands.CreateTopic;

public sealed record CreateTopicCommand(
    int ForumId,
    int CreatorUserId, 
    string Title,
    string Content) : IRequest<Result<TopicDto>>;

public class CreateTopicHandler(IForumRepository forumRepository, ITopicFactory topicFactory, ITopicRepository topicRepository) : IRequestHandler<CreateTopicCommand, Result<TopicDto>>
{
    public async Task<Result<TopicDto>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var forum = await forumRepository.GetForumByIdAsync(request.ForumId, cancellationToken);
            if (forum is null || forum.Deleted is not null)
            {
                return Result<TopicDto>.NotFound("Forum does not exist");
            }
            
            var thread = topicFactory.Create(request.Title, request.Content, request.ForumId, request.CreatorUserId);
            await topicRepository.AddTopic(thread);
            
            var response = thread.Adapt<TopicDto>();
            return Result<TopicDto>.Created(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            const string message = "An error occurred while creating thread";
            return Result<TopicDto>.CriticalError(message);
        }
    }
}