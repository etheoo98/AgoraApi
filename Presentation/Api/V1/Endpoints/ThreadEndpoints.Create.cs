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

public sealed record CreateThreadDto(string Title, string Content);

public partial class ThreadsEndpoints
{
    private void AddCreateThreadRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateThreadDto request,
            HttpContext httpContext,
            IValidator<CreateThreadCommand> validator, 
            ISender sender) =>
        {
            var userId = httpContext.GetUserId();
            var command = request.Adapt<CreateThreadCommand>() with { CreatorUserId = userId };;
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Create Thread").RequireAuthorization();
    }
}