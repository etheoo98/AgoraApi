using Domain.Common;
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
        thread.Deleted = DateTimeOffset.Now;
        thread.LastModified = DateTimeOffset.Now;
        
        var now = DateTimeOffset.Now;
        
        await context.Comments
            .Where(c => c.ThreadId == thread.Id)
            .ExecuteUpdateAsync(c => c
                    .SetProperty(comment => comment.Deleted, now),
                cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ContentDto>> SearchAsync(
        string? query,
        DateTimeOffset? updatedAfter,
        DateTimeOffset? startAfter,
        DateTimeOffset? startBefore,
        string? sortBy,
        string? searchAndOr,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var result = context.Threads.AsQueryable();

        if (!string.IsNullOrEmpty(query) && !string.IsNullOrEmpty(searchAndOr))
        {
            var searchTerms = query.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(term => term.ToLower());

            if (searchAndOr.Equals("and", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var term in searchTerms)
                {
                    result = result.Where(t =>
                        t.Title.ToLower().Contains(term) || t.Content.ToLower().Contains(term));
                }
            }
            else if (searchAndOr.Equals("or", StringComparison.OrdinalIgnoreCase))
            {
                result = result.Where(t =>
                    searchTerms.Any(term => t.Title.ToLower().Contains(term) || t.Content.ToLower().Contains(term)));
            }
        }

        if (updatedAfter.HasValue)
        {
            result = result.Where(t => t.LastModified >= updatedAfter.Value);
        }

        if (startAfter.HasValue)
        {
            result = result.Where(t => t.Created >= startAfter.Value);
        }

        if (startBefore.HasValue)
        {
            result = result.Where(t => t.Created <= startBefore.Value);
        }

        result = result
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        result = result.Where(r => r.Deleted == null);

        return await result.Select(t => new ContentDto
        {
            Id = t.Id,
            Title = t.Title,
            Content = t.Content,
            Created = t.Created,
            LastModified = t.LastModified
        }).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(string? query, CancellationToken cancellationToken)
    {
        var result = context.Threads.AsQueryable();
        
        if (!string.IsNullOrEmpty(query))
        {
            result = result.Where(t => 
                t.Title.Contains(query) || 
                t.Content.Contains(query));
        }
        
        return await result.CountAsync(cancellationToken);
    }
}