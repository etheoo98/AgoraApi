using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Email, 
    string Username, 
    string Password) : IRequest<Result<UserDto>>;