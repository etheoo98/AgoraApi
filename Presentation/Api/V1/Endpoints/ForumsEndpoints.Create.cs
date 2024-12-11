using Application.Forums.Commands;
using Application.Threads.Commands.CreateThread;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record CreateForumDto(string Name, string? Description);

public partial class ForumsEndpoints
{
    private void AddCreateForumRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateForumDto request,
            IValidator<CreateForumCommand> validator, 
            ISender sender) =>
        {
            var command = request.Adapt<CreateForumCommand>();
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Create Forum").RequireAuthorization();
    }
}