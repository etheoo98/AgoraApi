using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment, CancellationToken cancellationToken);
}