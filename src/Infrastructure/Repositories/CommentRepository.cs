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
}