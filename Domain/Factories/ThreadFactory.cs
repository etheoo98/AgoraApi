using Domain.Interfaces.Factories;
using Thread = Domain.Entities.Thread;

namespace Domain.Factories;

public class ThreadFactory :IThreadFactory
{
    public Thread Create(string title, string content, int userId)
    {
        return new Thread
        {
            Title = title,
            Content = content,
            UserId = userId,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
            IsDeleted = false,
            Deleted = null
        };
    }
}