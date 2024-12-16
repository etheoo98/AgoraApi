using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface ITopicFactory
{
    Topic Create(string title, string content, int forumId, int authorId);
}