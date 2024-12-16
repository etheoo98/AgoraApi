using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class TopicsEndpoints() : CarterModule("/")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddCreateTopicRoute(app);
        AddUpdateTopicRoute(app);
        AddDeleteTopicRoute(app);
    }
}