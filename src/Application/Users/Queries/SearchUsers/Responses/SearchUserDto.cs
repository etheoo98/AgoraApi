using Application.Users.Responses;

namespace Application.Users.Queries.SearchUsers.Responses;

public sealed record SearchUserDto(
    int NextPage, 
    int TotalItems, 
    List<UserDto> Users);