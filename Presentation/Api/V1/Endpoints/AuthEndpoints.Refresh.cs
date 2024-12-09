using Application.Auth.Commands.LoginUser;
using Application.Auth.Commands.LoginWithRefreshToken;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class AuthEndpoints
{
    public void AddRefreshRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            LoginWithRefreshToken request,
            IValidator<LoginWithRefreshTokenCommand> validator,
            ISender sender) =>
        {
            var command = request.Adapt<LoginWithRefreshTokenCommand>();
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        });
    }
}