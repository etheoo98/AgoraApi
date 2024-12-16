using Application.Topics.Commands.CreateTopic;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record CreateTopicDto(string Title, string Content);

public partial class TopicsEndpoints
{
    private void AddCreateTopicRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/forums/{forumId:int}/topics", async (
            int forumId,
            CreateTopicDto request,
            HttpContext httpContext,
            IValidator<CreateTopicCommand> validator, 
            ISender sender) =>
        {
            var userId = httpContext.GetUserId();
            var command = request.Adapt<CreateTopicCommand>() 
                with { CreatorUserId = userId, ForumId = forumId };
            
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