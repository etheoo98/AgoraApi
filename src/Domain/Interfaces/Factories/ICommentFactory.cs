using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface ICommentFactory
{
    Comment Create(string content, int topicId, int authorId);
}