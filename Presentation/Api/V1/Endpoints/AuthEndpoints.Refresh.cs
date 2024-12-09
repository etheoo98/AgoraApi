using Application.Auth.Commands.LoginWithRefreshToken;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public sealed record LoginWithRefreshTokenDto(string RefreshToken);

public partial class AuthEndpoints
{
    private void AddRefreshRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            LoginWithRefreshTokenDto request,
            IValidator<LoginWithRefreshToken> validator,
            ISender sender) =>
        {
            var command = request.Adapt<LoginWithRefreshToken>();
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Refresh Token");
    }
}