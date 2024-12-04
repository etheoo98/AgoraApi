using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    int Id,
    string? Email, 
    string? Username, 
    string? Password) : IRequest<Result<UserDto>>;