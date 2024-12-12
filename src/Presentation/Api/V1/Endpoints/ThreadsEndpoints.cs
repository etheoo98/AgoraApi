﻿using Carter;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class ThreadsEndpoints() : CarterModule("/")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        AddCreateThreadRoute(app);
        AddUpdateThreadRoute(app);
    }
}