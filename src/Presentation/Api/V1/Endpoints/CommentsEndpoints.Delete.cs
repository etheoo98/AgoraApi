using Application.Comments.Commands.DeleteComment;
using Ardalis.Result.AspNetCore;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public partial class CommentsEndpoints
{
    private static void AddDeleteCommentRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/comment/{commentId:int}", async (
            int commentId,
            HttpContext context,
            IValidator<DeleteCommentCommand> validator,
            ISender sender) =>
        {
            var userId = context.GetUserId();
            var command = new DeleteCommentCommand(userId, commentId);
            
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