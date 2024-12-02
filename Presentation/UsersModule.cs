using Application.Users.GetUserById;
using Ardalis.Result.AspNetCore;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation;

public class UsersModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", async (
            int id, 
            IValidator<GetUserByIdQuery> validator, 
            ISender sender) =>
        {
            var query = new GetUserByIdQuery(id);
            var validation = await validator.ValidateAsync(query);
        
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(query);
            return result.ToMinimalApiResult();
        }).WithName("Get User by Id");
    }
}