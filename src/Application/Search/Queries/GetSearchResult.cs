using Application.Search.Responses;
using Application.Users.Responses;
using Ardalis.Result;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Search.Queries;

public sealed record GetSearchResultQuery(
    string? Query,
    string? Type,
    string? SortBy,
    string? SearchAndOr,
    DateTimeOffset? UpdatedAfter,
    DateTimeOffset? StartAfter,
    DateTimeOffset? StartBefore,
    DateTimeOffset? JoinDate,
    int Page,
    int PageSize
    ) : IRequest<Result<SearchResponseDto>>;

public class GetSearchResultHandler(
    IUserRepository userRepository,
    IForumRepository forumRepository,
    ITopicRepository topicRepository,
    ICommentRepository commentRepository
    ) : IRequestHandler<GetSearchResultQuery, Result<SearchResponseDto>>
{
    public async Task<Result<SearchResponseDto>> Handle(GetSearchResultQuery request, CancellationToken cancellationToken)
    {
        return request.Type?.ToLower() switch
        {
            "users" => await HandleMemberSearch(request, cancellationToken),
            "topics" or "comments" or "forums" => await HandleContentSearch(request, cancellationToken),
            _ => throw new ArgumentException($"Invalid search type: {request.Type}")
        };
    }
    
    private async Task<Result<SearchResponseDto>> HandleContentSearch(GetSearchResultQuery request, CancellationToken cancellationToken)
    {
        IContentRepository repository = request.Type switch
        {
            "topics" => topicRepository,
            "comments" => commentRepository,
            _ => throw new ArgumentException($"Unsupported content type: {request.Type}")
        };

        var results = await repository.SearchAsync(
            request.Query,
            request.UpdatedAfter,
            request.StartAfter,
            request.StartBefore,
            request.SortBy,
            request.SearchAndOr,
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var totalCount = await repository.CountAsync(request.Query, cancellationToken);

        return Result.Success(new SearchResponseDto(
            results,
            totalCount,
            request.Page,
            request.PageSize
        ));
    }

    private async Task<Result<SearchResponseDto>> HandleMemberSearch(GetSearchResultQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.SearchUsersAsync(
            request.Query,
            request.JoinDate,
            request.Page,
            request.PageSize,
            cancellationToken);

        return Result.Success(new SearchResponseDto(
            users.Adapt<List<UserDto>>(),
            users.Count,
            request.Page,
            request.PageSize
        ));
    }
}