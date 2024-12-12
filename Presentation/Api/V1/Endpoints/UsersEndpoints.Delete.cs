using Application.Common.Validators;
using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetUserById;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class UsersEndpoints
{
    private void AddDeleteUserRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", async (
            int id,
            IValidator<DeleteUserCommand> validator, 
            ISender sender) =>
        {
            var command = new DeleteUserCommand(id);
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Delete User");
    }
}