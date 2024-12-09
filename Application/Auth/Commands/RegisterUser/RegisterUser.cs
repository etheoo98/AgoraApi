using Application.Users.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Application.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Username, 
    string Password) : IRequest<Result<UserDto>>;
    
public class RegisterUserHandler(IUserFactory userFactory, IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHash = BC.HashPassword(request.Password);
            var user = userFactory.CreateUser(request.Email, request.Username, passwordHash);
            await userRepository.AddUser(user, cancellationToken);
            var response = user.Adapt<UserDto>();
            return Result<UserDto>.Success(response);
        }
        catch (Exception ex)
        {
            const string? message = "An error occurred while creating user";
            return Result<UserDto>.CriticalError(message);
        }
    }
}