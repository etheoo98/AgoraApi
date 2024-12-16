using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<User?> GetUserByCredentialsAsync(string? email, string? username, string password);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    Task UpdateLastLoginDateAsync(User user);
}