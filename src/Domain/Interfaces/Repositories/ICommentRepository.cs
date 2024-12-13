using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment, CancellationToken cancellationToken);
    Task<Comment?> GetCommentByIdAsync(int id, CancellationToken cancellationToken);
    public Task UpdateCommentAsync(Comment comment, string content, CancellationToken cancellationToken);
    public Task DeleteCommentAsync(Comment comment, CancellationToken cancellationToken);
}