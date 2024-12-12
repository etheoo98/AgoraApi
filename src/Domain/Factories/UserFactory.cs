using Domain.Entities;
using Domain.Interfaces.Factories;

namespace Domain.Factories;

public class UserFactory : IUserFactory
{
    public User CreateUser(string email, string username, string password)
    {
        return new User
        {
            Email = email,
            Username = username,
            Password = password,
            LastLogin = null,
            Created = DateTimeOffset.Now,
            LastModified = DateTimeOffset.Now,
            IsDeleted = false,
            Deleted = null
        };
    }
}