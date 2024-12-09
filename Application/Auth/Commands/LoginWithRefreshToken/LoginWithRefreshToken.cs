﻿using Application.Auth.Responses;
using Application.Auth.Services;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Auth.Commands.LoginWithRefreshToken;

public sealed record LoginWithRefreshToken(string RefreshToken) : IRequest<Result<TokenDto>>;

public class LoginWithRefreshTokenHandler (IAuthRepository authRepository, ITokenGenerator tokenGenerator) : IRequestHandler<LoginWithRefreshToken, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(LoginWithRefreshToken request, CancellationToken cancellationToken)
    {
        try
        {
            var refreshToken = await authRepository.GetRefreshTokenByTokenAsync(request.RefreshToken);
            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                return Result<TokenDto>.Unauthorized("Invalid refresh token");
            }
            
            var accessToken = tokenGenerator.GenerateToken(refreshToken.User.Id, refreshToken.User.Username);
            
            refreshToken.Token = tokenGenerator.GenerateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(30);
            await authRepository.UpdateRefreshTokenAsync(refreshToken);
            
            var response = new TokenDto(accessToken, refreshToken.Token);
            return Result<TokenDto>.Success(response);
        }
        catch (Exception e)
        {
            const string? message = "An error occurred while logging in user with refresh token.";
            Console.WriteLine(e.Message);
            return Result<TokenDto>.CriticalError(message);
        }
    }
}