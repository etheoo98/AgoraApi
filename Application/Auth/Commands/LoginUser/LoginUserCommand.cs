using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Auth.Commands.LoginUser;

public sealed record LoginUserCommand(
    string? Email,
    string? Username,
    string Password) : IRequest<Result<TokenDto>>;