using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class ForumFactory : IForumFactory
{
    public Forum CreateForum(string name, string description)
    {
        return new Forum
        {
            Name = name,
            Description =  description,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now
        };
    }
}