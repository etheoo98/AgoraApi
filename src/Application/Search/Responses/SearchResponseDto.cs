namespace Application.Search.Responses;

public sealed record SearchResponseDto(
    object Result,
    int TotalCount,
    int Page,
    int PageSize
);