namespace Application.Common.Models;

public sealed record UserDto(
    int Id,
    string Username,
    DateTimeOffset? LastLogin,
    DateTimeOffset Created,
    DateTimeOffset LastModified,
    bool IsDeleted,
    DateTimeOffset? Deleted);