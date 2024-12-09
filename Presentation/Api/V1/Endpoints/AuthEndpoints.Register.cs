using Application.Auth.Commands.RegisterUser;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class AuthEndpoints
{
    private void AddRegisterRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (
            RegisterUserCommand request,
            IValidator<RegisterUserCommand> validator,
            ISender sender) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            var result = await sender.Send(request);
            return result.ToMinimalApiResult();
        }).WithName("Register");
    }
}