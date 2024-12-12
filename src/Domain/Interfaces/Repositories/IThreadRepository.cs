using Thread = Domain.Entities.Thread;

namespace Domain.Interfaces.Repositories;

public interface IThreadRepository
{
    Task AddThread(Thread thread);
    Task<Thread?> GetThreadByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateThread(Thread thread, string title, string content, CancellationToken cancellationToken);
    Task<bool> ThreadExists(int threadId, CancellationToken cancellationToken);
    Task DeleteThread(Thread thread, CancellationToken cancellationToken);
}