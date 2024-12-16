using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class TopicFactory :ITopicFactory
{
    public Topic Create(string title, string content, int forumId, int authorId)
    {
        return new Topic
        {
            Title = title,
            Content = content,
            ForumId = forumId,
            AuthorId = authorId,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
            Deleted = null
        };
    }
}