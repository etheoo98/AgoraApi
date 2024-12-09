using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class AuthEndpoints() : CarterModule("/auth")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddLoginRoute(app);
        AddRefreshRoute(app);
    }
}