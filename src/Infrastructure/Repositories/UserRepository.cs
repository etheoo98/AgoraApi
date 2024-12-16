using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteUser(User user, CancellationToken cancellationToken)
    {
        user.IsDeleted = true;
        user.Deleted = DateTimeOffset.Now;
        user.LastModified = DateTimeOffset.Now;

        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<User>> SearchUsersAsync(string? searchTerm,
        DateTimeOffset? joinDate,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Username.Contains(searchTerm));
        }

        query = query.Where(u => u.Deleted == null);

        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountUsersAsync(string? searchTerm, CancellationToken cancellationToken)
    {
        var query = context.Users.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Username.Contains(searchTerm));
        }

        return await query.CountAsync(cancellationToken);
    }
}