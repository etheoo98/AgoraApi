using Application.Threads.Commands.UpdateThread;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record UpdateThreadDto(string Title, string Content);

public partial class ThreadsEndpoints
{
    private void AddUpdateThreadRoute(IEndpointRouteBuilder app)
    {
        app.MapPatch("/threads/{threadId:int}", async (
            int threadId, 
            UpdateThreadDto request,
            HttpContext context,
            IValidator<UpdateThreadCommand> validator, 
            ISender sender) =>
        {
            var userId = context.GetUserId();
            var command = request.Adapt<UpdateThreadCommand>() with { ThreadId = threadId, UserId = userId };
            
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Update Thread").RequireAuthorization();
    }
}