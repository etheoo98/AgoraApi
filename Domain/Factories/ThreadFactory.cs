using Domain.Interfaces.Factories;
using Thread = Domain.Entities.Thread;

namespace Domain.Factories;

public class ThreadFactory :IThreadFactory
{
    public Thread Create(string title, string content, int forumId, int creatorId)
    {
        return new Thread
        {
            Title = title,
            Content = content,
            ForumId = forumId,
            CreatorId = creatorId,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
            IsDeleted = false,
            Deleted = null
        };
    }
}