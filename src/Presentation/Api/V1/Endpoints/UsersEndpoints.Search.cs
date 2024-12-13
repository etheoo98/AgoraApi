using Application.Users.Queries.SearchUsers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Api.V1.Endpoints;

public partial class UsersEndpoints
{
    private static void AddSearchUsersRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            string? searchTerm,
            int? page,
            int? pageSize,
            ISender sender
            ) =>
        {
            if (pageSize is <= 0 or >= 30)
            {
                page ??= 1;
            }
            
            var command = new SearchUsersQuery(searchTerm, page ?? 1, pageSize ?? 4);
            return await sender.Send(command);
        });
    }
}