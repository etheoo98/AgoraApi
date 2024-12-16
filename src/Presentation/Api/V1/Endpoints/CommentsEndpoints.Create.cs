using Application.Comments.Commands.CreateComment;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record CreateCommentDto(string Content);

public partial class CommentsEndpoints
{
    private void AddCreateCommentRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/topics/{topicId}/comments", async (
            int topicId,
            CreateCommentDto request,
            HttpContext context,
            IValidator<CreateCommentCommand> validator,
            ISender sender
        ) =>
        {
            var userId = context.GetUserId();
            var command = request.Adapt<CreateCommentCommand>()
                with { TopicId = topicId, AuthorId = userId };
            
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).RequireAuthorization();
    }
}