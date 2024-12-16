using Application.Search.Queries;
using Ardalis.Result.AspNetCore;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public class SearchEndpoints() : CarterModule("/search")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            ISender sender,
            string? q,
            string? type,
            string? updatedAfter,
            string? startAfter,
            string? startBefore,
            string? joinDate,
            int? page,
            int? pageSize,
            string sortBy = "relevancy",
            string searchAndOr = "and"
            ) =>
        {
            var query = new GetSearchResultQuery(
                q,
                type,
                sortBy,
                searchAndOr,
                ParseNullableDateTime(updatedAfter),
                ParseNullableDateTime(startAfter),
                ParseNullableDateTime(startBefore),
                ParseNullableDateTime(joinDate),
                page ?? 1,
                pageSize ?? 24
            );
            
            var result = await sender.Send(query);
            return result.ToMinimalApiResult();
        });
    }
    
    private static DateTimeOffset? ParseNullableDateTime(string? dateTime)
    {
        return string.IsNullOrEmpty(dateTime) ? null : DateTimeOffset.Parse(dateTime);
    }
}