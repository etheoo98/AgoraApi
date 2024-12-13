using Application.Comments.Commands.UpdateComment;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record UpdateCommentDto(string Content);

public partial class CommentsEndpoints
{
    private static void AddUpdateCommentRoute(IEndpointRouteBuilder app)
    {
        app.MapPatch("/comments/{commentId:int}", async (
            int commentId, 
            UpdateCommentDto request,
            HttpContext context,
            IValidator<UpdateCommentCommand> validator, 
            ISender sender) =>
        {
            var userId = context.GetUserId();
            var command = request.Adapt<UpdateCommentCommand>() with { CommentId = commentId, UserId = userId };
            
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }
            
            var result = await sender.Send(command);
            return result.ToMinimalApiResult();
        }).WithName("Update Comment").RequireAuthorization();
    }
}