using Domain.Entities;
using Domain.Interfaces.Factories;
using BC = BCrypt.Net.BCrypt;

namespace Domain.Factories;

public class UserFactory : IUserFactory
{
    public User Create(string email, string username, string password)
    {
        return new User
        {
            Email = email,
            Username = username,
            Password = BC.HashPassword(password),
            LastLogin = null,
            Created = DateTimeOffset.UtcNow,
            LastModified = DateTimeOffset.UtcNow,
            IsDeleted = false,
            Deleted = null,
        };
    }
}