using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ForumRepository(ApplicationDbContext context) : IForumRepository
{
    public async Task AddForumAsync(Forum forum, CancellationToken cancellationToken)
    {
        await context.Forums.AddAsync(forum, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}