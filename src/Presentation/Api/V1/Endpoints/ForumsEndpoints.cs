using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class ForumsEndpoints() : CarterModule("/forums")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddCreateForumRoute(app);
    }
}