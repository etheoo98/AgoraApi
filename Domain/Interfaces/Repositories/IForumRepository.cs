using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IForumRepository
{
    Task AddForumAsync(Forum forum, CancellationToken cancellationToken);
}