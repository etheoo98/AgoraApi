using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class AuthEndpoints() : CarterModule("/auth")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddRegisterRoute(app);
        AddLoginRoute(app);
        AddRefreshRoute(app);
    }
}