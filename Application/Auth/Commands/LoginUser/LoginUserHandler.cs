using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.Result;
using BC = BCrypt.Net.BCrypt;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Auth.Commands.LoginUser;

public class LoginUserHandler(IAuthRepository authRepository, ITokenGenerator tokenGenerator) : IRequestHandler<LoginUserCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await authRepository.GetUserByCredentialsAsync(request.Email, request.Username, request.Password);
            if (user is null || !BC.Verify(request.Password, user.Password))
            {
                return Result<TokenDto>.Unauthorized();
            }
            
            var accessToken = tokenGenerator.GenerateToken(user.Id, user.Username);
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = tokenGenerator.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(30)
            };
            
            await authRepository.AddRefreshTokenAsync(refreshToken);
            var response = new TokenDto(accessToken, refreshToken.Token);
            return Result<TokenDto>.Success(response);
        }
        catch (Exception e)
        {
            const string? message = "An error occurred while logging in user";
            Console.WriteLine(e.Message);
            return Result<TokenDto>.CriticalError(message);
        }
    }
}