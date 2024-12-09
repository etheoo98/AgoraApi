using Microsoft.Extensions.Configuration;

namespace Application.Common.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(int userId, string username);
    string GenerateRefreshToken();
}