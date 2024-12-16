namespace Application.Topics.Responses;

public sealed record TopicDto(
    int Id, 
    string Title, 
    string Content, 
    DateTimeOffset Created,
    DateTimeOffset LastModified,
    bool IsDeleted,
    DateTimeOffset? Deleted);