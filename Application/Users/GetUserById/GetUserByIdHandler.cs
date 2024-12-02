using Ardalis.Result;
using Domain.Repositories.Interfaces;
using MediatR;

namespace Application.Users.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository repository)
    : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
{
    // TODO: Add logger
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.FetchUserById(request.Id);
            var userResponse = new UserResponse(user.Id, user.Username);
            return Result<UserResponse>.Success(userResponse);
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while retrieving user by ID";
            return Result<UserResponse>.CriticalError(message);
        }
    }
}