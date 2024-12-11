using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Presentation.Api.V1.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        if (context.User.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        
        var userIdClaim = identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User ID claim (sub) is missing.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new FormatException($"Invalid user ID format in 'sub' claim: {userIdClaim.Value}");
        }

        return userId;
    }
}