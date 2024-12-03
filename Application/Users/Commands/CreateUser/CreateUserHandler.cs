using Application.Common.Models;
using Ardalis.Result;
using Domain.Factories;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserFactory userFactory, IUserRepository repository) : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = userFactory.Create(request.Email, request.Username, request.Password);
            await repository.AddUser(user);
            var userResponse = new UserDto(user.Id, user.Username);
            return Result<UserDto>.Success(userResponse);
        }
        catch (Exception ex)
        {
            const string? message = "An error occurred while creating user";
            return Result<UserDto>.CriticalError(message);
        }
    }
}