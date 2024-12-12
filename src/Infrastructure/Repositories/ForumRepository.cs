using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ForumRepository(ApplicationDbContext context) : IForumRepository
{
    public async Task AddForumAsync(Forum forum, CancellationToken cancellationToken)
    {
        await context.Forums.AddAsync(forum, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Forum?> GetForumByIdAsync(int forumId, CancellationToken cancellationToken)
    {
        return await context.Forums.FirstOrDefaultAsync(x => x.Id == forumId, cancellationToken);
    }

    public async Task<bool> ForumExistsAsync(int forumId, CancellationToken cancellationToken)
    {
        return await context.Forums.AnyAsync(forum => forum.Id == forumId, cancellationToken);
    }
}