using Domain.Models;
using Domain.Repositories.Interfaces;

namespace Domain.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<User?> FetchUserById(int id)
    {
        return new User
        {
            Id = 1,
            Username = $"User{id}",
        };
    }
}