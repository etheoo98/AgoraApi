using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddUser(User user, CancellationToken cancellationToken);
    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task DeleteUser(User user, CancellationToken cancellationToken);
}