using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface IUserFactory
{
    User CreateUser(string email, string username, string password);
}