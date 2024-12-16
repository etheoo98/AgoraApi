using Thread = Domain.Entities.Thread;

namespace Domain.Interfaces.Repositories;

public interface IThreadRepository : IContentRepository
{
    Task AddThread(Thread thread);
    Task<Thread?> GetThreadByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateThread(Thread thread, string title, string content, CancellationToken cancellationToken);
    Task<bool> ThreadExists(int threadId, CancellationToken cancellationToken);
    Task DeleteThreadAndComments(Thread thread, CancellationToken cancellationToken);
}