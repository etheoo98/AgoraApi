using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Api.V1.Extensions;

namespace Presentation.Api.V1.Endpoints;

public sealed record CreateCommentDto(string Content);

public partial class CommentsEndpoints() : CarterModule("/")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddCreateCommentRoute(app);
        AddUpdateCommentRoute(app);
    }
}