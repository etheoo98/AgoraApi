using Application.Common.Models;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository repository)
    : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetUserByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result<UserDto>.NotFound("User not found");
            }
            
            var userResponse = new UserDto(user.Id, user.Username);
            return Result<UserDto>.Success(userResponse);
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while retrieving user by ID";
            // TODO: Add logger?
            return Result<UserDto>.CriticalError(message);
        }
    }
}