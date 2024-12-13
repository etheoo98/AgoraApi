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

    public async Task UpdateThread(Thread thread, string? title, string? content, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(title))
        {
            thread.Title = title;
        }

        if (!string.IsNullOrEmpty(content))
        {
            thread.Content = content;
        }
        
        thread.LastModified = DateTimeOffset.Now;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ThreadExists(int threadId, CancellationToken cancellationToken)
    {
        return await context.Threads.AnyAsync(c => c.Id == threadId, cancellationToken);
    }

    public async Task DeleteThreadAndComments(Thread thread, CancellationToken cancellationToken)
    {
        thread.IsDeleted = true;
        thread.Deleted = DateTimeOffset.Now;
        thread.LastModified = DateTimeOffset.Now;
        
        var now = DateTimeOffset.Now;
        
        await context.Comments
            .Where(c => c.ThreadId == thread.Id)
            .ExecuteUpdateAsync(c => c
                    .SetProperty(comment => comment.IsDeleted, true)
                    .SetProperty(comment => comment.Deleted, now),
                cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}