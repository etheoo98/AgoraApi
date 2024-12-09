using Application.Users.Commands.UpdateUser;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public sealed record UpdateUserDto(
    string? Email, 
    string? Username, 
    string? Password);

public partial class UserEndpoints
{
    private void AddUpdateUserRoute(IEndpointRouteBuilder app)
    {
        app.MapPatch("/{id:int}", async (
            int id, 
            UpdateUserDto request,
            IValidator<UpdateUserCommand> validator, 
            ISender sender) =>
        {
            var command = new UpdateUserCommand(id, request.Email, request.Username, request.Password);
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Update User");
    }
}