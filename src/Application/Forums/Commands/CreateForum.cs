using Application.Forums.Responses;
using Ardalis.Result;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Repositories;
using Mapster;
using MediatR;

namespace Application.Forums.Commands;

public sealed record CreateForumCommand(
    string Name, 
    string Description) : IRequest<Result<ForumDto>>;

public class CreateForumHandler(IForumFactory forumFactory, IForumRepository forumRepository) : IRequestHandler<CreateForumCommand, Result<ForumDto>>
{
    public async Task<Result<ForumDto>> Handle(CreateForumCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var forum = forumFactory.CreateForum(request.Name, request.Description);
            await forumRepository.AddForumAsync(forum, cancellationToken);
            
            var response = forum.Adapt<ForumDto>();
            return Result<ForumDto>.Success(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            const string message = "An error occurred while creating forum";
            return Result<ForumDto>.CriticalError(message);
        }
    }
}