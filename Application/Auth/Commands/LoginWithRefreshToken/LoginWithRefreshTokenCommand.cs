using Application.Common.Models;
using Ardalis.Result;
using MediatR;

namespace Application.Auth.Commands.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenDto>>;