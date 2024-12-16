using Application.Topics.Commands.UpdateThread;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record UpdateTopicDto(string Title, string Content);

public partial class TopicsEndpoints
{
    private void AddUpdateTopicRoute(IEndpointRouteBuilder app)
    {
        app.MapPatch("/topics/{topicId:int}", async (
            int topicId, 
            UpdateTopicDto request,
            HttpContext context,
            IValidator<UpdateTopicCommand> validator, 
            ISender sender) =>
        {
            var userId = context.GetUserId();
            var command = request.Adapt<UpdateTopicCommand>() with { TopicId = topicId, UserId = userId };
            
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