using Domain.Factories;
using Domain.Interfaces.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserFactory, UserFactory>();
        services.AddScoped<IForumFactory, ForumFactory>();
        services.AddScoped<IThreadFactory, ThreadFactory>();
        services.AddScoped<ICommentFactory, CommentFactory>();
        
        return services;
    }
}