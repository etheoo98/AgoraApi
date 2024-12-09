﻿using Ardalis.Result;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public class ExampleEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (ISender sender) =>
        {
            var result = Result<string>.Success("Hello World");
            return Task.FromResult(result);
        }).RequireAuthorization();
    }
}