using Application.Common.Models;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using Mapster;
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
            
            var response = user.Adapt<UserDto>();
            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while retrieving user by ID";
            // TODO: Add logger?
            return Result<UserDto>.CriticalError(message);
        }
    }
}