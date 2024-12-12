using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CommentRepository(ApplicationDbContext context) : ICommentRepository
{
    public async Task AddCommentAsync(Comment comment, CancellationToken cancellationToken)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}