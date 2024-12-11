using Thread = Domain.Entities.Thread;

namespace Domain.Interfaces.Factories;

public interface IThreadFactory
{
    Thread Create(string title, string content, int forumId, int creatorId);
}