using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Thread = Domain.Entities.Thread;

namespace Infrastructure.Repositories;

public class ThreadRepository(ApplicationDbContext context) : IThreadRepository
{
    public async Task AddThread(Thread thread)
    {
        await context.Threads.AddAsync(thread);
        await context.SaveChangesAsync();
    }
}