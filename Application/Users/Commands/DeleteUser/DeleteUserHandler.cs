using Ardalis.Result;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.NotFound("User not found");
            }
            
            SoftDeleteUser(user);
            await userRepository.UpdateUserAsync(user, cancellationToken);
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            const string? message = "An error occurred while deleting user";
            return Result.CriticalError(message);
        }
    }

    private void SoftDeleteUser(User user)
    {
        user.IsDeleted = true;
        user.Deleted = DateTimeOffset.Now;
        user.LastModified = DateTimeOffset.Now;
    }
}