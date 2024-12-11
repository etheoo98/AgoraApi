using Domain.Entities;

namespace Domain.Interfaces.Factories;

public interface IForumFactory
{
    Forum CreateForum(string name, string description);
}