using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> AddUser(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(user);
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Users.FindAsync([id], cancellationToken);
    }
}