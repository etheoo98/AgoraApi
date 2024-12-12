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

    public async Task<bool> ThreadExists(int threadId, CancellationToken cancellationToken)
    {
        return await context.Threads.AnyAsync(c => c.Id == threadId, cancellationToken);
    }
}