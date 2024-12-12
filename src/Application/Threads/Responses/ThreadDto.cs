namespace Application.Threads.Responses;

public sealed record ThreadDto(
    int Id, 
    string Title, 
    string Content, 
    DateTimeOffset Created,
    DateTimeOffset LastModified,
    bool IsDeleted,
    DateTimeOffset? Deleted);