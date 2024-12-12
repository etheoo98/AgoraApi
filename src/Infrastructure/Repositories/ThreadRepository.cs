using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Thread = Domain.Entities.Thread;

namespace Infrastructure.Repositories;

public class ThreadRepository(ApplicationDbContext context) : IThreadRepository
{
    public async Task AddThread(Thread thread)
    {
        await context.Threads.AddAsync(thread);
        await context.SaveChangesAsync();
    }

    public async Task<Thread?> GetThreadByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Threads.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateThread(Thread thread, CancellationToken cancellationToken)
    {
        context.Threads.Update(thread);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ThreadExists(int threadId, CancellationToken cancellationToken)
    {
        return await context.Threads.AnyAsync(c => c.Id == threadId, cancellationToken);
    }
}