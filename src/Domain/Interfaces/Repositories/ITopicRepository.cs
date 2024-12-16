using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITopicRepository : IContentRepository
{
    Task AddTopic(Topic topic);
    Task<Topic?> GetTopicByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateTopic(Topic topic, string title, string content, CancellationToken cancellationToken);
    Task<bool> TopicExists(int threadId, CancellationToken cancellationToken);
    Task DeleteTopicAndComments(Topic topic, CancellationToken cancellationToken);
}