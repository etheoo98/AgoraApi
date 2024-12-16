using Domain.Common;

namespace Domain.Interfaces.Repositories;

public interface IContentRepository
{
    Task<List<ContentDto>> SearchAsync(
        string? query,
        DateTimeOffset? updatedAfter,
        DateTimeOffset? startAfter,
        DateTimeOffset? startBefore,
        string? sortBy,
        string? searchAndOr,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountAsync(string? query, CancellationToken cancellationToken);
}