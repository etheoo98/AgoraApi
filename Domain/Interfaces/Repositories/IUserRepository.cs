using Domain.Entities;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task<User> AddUser(User user);
    Task<User?> FetchUserById(int id);
}