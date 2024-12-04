using Application.Users.Commands.CreateUser;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class UsersEndpoint
{
    private void AddCreateUserRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateUserCommand request,
            IValidator<CreateUserCommand> validator, 
            ISender sender) =>
        {
            var validation = await validator.ValidateAsync(request);
        
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(request);
            return result.ToMinimalApiResult();
        }).WithName("Create User");
    }
}