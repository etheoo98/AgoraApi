using Application.Common.Models;
using Ardalis.Result;
using BC = BCrypt.Net.BCrypt;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await repository.GetUserByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result<UserDto>.NotFound($"User with ID {request.Id} not found.");
            }
            
            UpdateUserProperties(user, request);
            await repository.UpdateUserAsync(user, cancellationToken);
            var response = user.Adapt<UserDto>();
            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            const string? message = "An error occurred while updating user";
            return Result<UserDto>.CriticalError(message);
        }
    }

    private void UpdateUserProperties(User user, UpdateUserCommand request)
    {
        if (!string.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Username))
        {
            user.Username = request.Username;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            var passwordHash = BC.HashPassword(request.Password);
            user.Password = passwordHash;
        }
        
        user.LastModified = DateTimeOffset.UtcNow;
    }
}