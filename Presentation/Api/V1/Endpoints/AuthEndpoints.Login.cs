using Application.Auth.Commands.LoginUser;
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
    public void AddLoginRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
            LoginDto loginDto,
            IValidator<LoginUserCommand> validator,
            ISender sender) =>
        {
            var command = loginDto.Adapt<LoginUserCommand>();
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