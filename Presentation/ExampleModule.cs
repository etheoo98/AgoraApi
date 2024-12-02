using Ardalis.Result;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation;

public class ExampleModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", (ISender sender) =>
        {
            var result = Result<string>.Success("Hello World");
            return Task.FromResult(result);
        });
    }
}