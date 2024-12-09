using Domain.Factories;
using Domain.Interfaces.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserFactory, UserFactory>();
        
        return services;
    }
}