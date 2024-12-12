using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository(ApplicationDbContext context) : IAuthRepository
{
    public async Task<User?> GetUserByCredentialsAsync(string? email, string? username, string password)
    {
        return await context.Users
            .SingleOrDefaultAsync(u => 
                (email != null && u.Email == email) || 
                (username != null && u.Username == username));
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();
    }

    public Task<RefreshToken?> GetRefreshTokenByTokenAsync(string refreshToken)
    {
        return context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        await context.SaveChangesAsync();
    }
}