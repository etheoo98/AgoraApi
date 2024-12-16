using Domain.Common;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    public async Task AddCommentAsync(Comment comment, CancellationToken cancellationToken)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Comment?> GetCommentByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Comments.FindAsync([id], cancellationToken);
    }

    public Task UpdateCommentAsync(Comment comment, string content, CancellationToken cancellationToken)
    {
        comment.Content = content;
        context.Comments.Update(comment);
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCommentAsync(Comment comment, CancellationToken cancellationToken)
    {
        comment.Deleted = DateTimeOffset.Now;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Comment>> SearchCommentsAsync(string? searchTerm, DateTime? joinDate, string? sortBy, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Comments.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t => 
                t.Content.Contains(searchTerm));
        }
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<int> CountCommentsAsync(string? searchTerm, CancellationToken cancellationToken)
    {
        var query = context.Comments.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t => 
                t.Content.Contains(searchTerm));
        }
        
        return await query.CountAsync(cancellationToken);
    }

    public async Task<List<ContentDto>> SearchAsync(string? query, DateTimeOffset? updatedAfter, DateTimeOffset? startAfter, DateTimeOffset? startBefore, string? sortBy,
        string? searchAndOr, int page, int pageSize, CancellationToken cancellationToken)
    {
        var result = context.Comments.AsQueryable();

        if (!string.IsNullOrEmpty(query) && !string.IsNullOrEmpty(searchAndOr))
        {
            var searchTerms = query.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToLower());

            if (searchAndOr.Equals("and", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var term in searchTerms)
                {
                    result = result.Where(t =>
                        t.Content.ToLower().Contains(term));
                }
            }
            else if (searchAndOr.Equals("or", StringComparison.OrdinalIgnoreCase))
            {
                result = result.Where(c =>
                    searchTerms.Any(term => c.Content.ToLower().Contains(term)));
            }
        }

        if (updatedAfter.HasValue)
        {
            result = result.Where(t => t.LastModified >= updatedAfter);
        }

        if (startAfter.HasValue)
        {
            result = result.Where(t => t.Created >= startAfter);
        }

        if (startBefore.HasValue)
        {
            result = result.Where(t => t.Created <= startBefore);
        }

        result = result
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        result = result.Where(r => r.Deleted == null);

        return await result.Select(t => new ContentDto
        {
            Id = t.Id,
            Title = "Comment",
            Content = t.Content,
            Created = t.Created,
            LastModified = t.LastModified
        }).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(string? query, CancellationToken cancellationToken)
    {
        var result = context.Comments.AsQueryable();
        
        if (!string.IsNullOrEmpty(query))
        {
            result = result.Where(t => 
                t.Content.Contains(query));
        }
        
        return await result.CountAsync(cancellationToken);
    }
}