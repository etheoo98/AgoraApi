using Application.Common.Models;
using Ardalis.Result;
using BC = BCrypt.Net.BCrypt;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserFactory userFactory, IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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