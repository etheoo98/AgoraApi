using Application.Users.Queries.SearchUsers.Responses;
using Application.Users.Responses;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Users.Queries.SearchUsers;

public sealed record SearchUsersQuery(string? SearchTerm, int Page, int PageSize) : IRequest<SearchUserDto>;

public class SearchUsersHandler(IUserRepository userRepository) : IRequestHandler<SearchUsersQuery, SearchUserDto>
{
    public async Task<SearchUserDto> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await userRepository.SearchUsersAsync(
                request.SearchTerm, request.Page, request.PageSize, cancellationToken);
            
            var userCount = await userRepository.CountUsersAsync(
                request.SearchTerm, cancellationToken);
            
            var nextPage = 0;
            if (request.Page * request.PageSize < userCount) 
            {
                nextPage = request.Page + 1;
            }
            
            var userDtos = users.Adapt<List<UserDto>>();
            var response = new SearchUserDto(nextPage, userCount, userDtos);
            
            return Result<SearchUserDto>.Success(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<SearchUserDto>.CriticalError("An error occured while searching users.");
        }
    }
}