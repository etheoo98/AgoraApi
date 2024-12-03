using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> AddUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return await Task.FromResult(user);
    }

    public async Task<User?> FetchUserById(int id)
    {
        return await context.Users.FindAsync(id);
    }
}