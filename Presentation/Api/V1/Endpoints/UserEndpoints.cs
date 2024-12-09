using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class UserEndpoints() : CarterModule("/users")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddCreateUserRoute(app);
        AddGetUserByIdRoute(app);
        AddUpdateUserRoute(app);
        AddDeleteUserRoute(app);
    }
}