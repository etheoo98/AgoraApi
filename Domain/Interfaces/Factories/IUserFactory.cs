using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface IUserFactory
{
    User Create(string email, string username, string password);
}