using Domain.Models;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository : IRepository
{
    Task<User?> FetchUserById(int id);
}