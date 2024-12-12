using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class CommentFactory : ICommentFactory
{
    public Comment Create(string content, int threadId, int authorId)
    {
        return new Comment
        {
            Content = content,
            ThreadId = threadId,
            AuthorId = authorId,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
        };
    }
}