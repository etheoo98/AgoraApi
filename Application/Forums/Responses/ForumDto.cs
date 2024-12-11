namespace Application.Forums.Responses;

public sealed record ForumDto(
    int Id, 
    string Name, 
    string Description);