using Application.Topics.Commands.DeleteThread;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public partial class TopicsEndpoints
{
    private void AddDeleteTopicRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/topics/{id:int}", async (
            int id,
            HttpContext context,
            IValidator<DeleteTopicCommand> validator, 
            ISender sender) =>
        {
            var userId = context.GetUserId();
            var command = new DeleteTopicCommand(id, userId);
            
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Delete Thread");
    }
}