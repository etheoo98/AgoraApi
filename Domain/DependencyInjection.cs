using Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        var repositoryTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.GetInterfaces().Any(i => i == typeof(IRepository) || i.Name == "I" + t.Name))
            .ToList();

        foreach (var implementationType in repositoryTypes)
        {
            var interfaceType = implementationType.GetInterfaces()
                .FirstOrDefault(i => i == typeof(IRepository) || i.Name == "I" + implementationType.Name);

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }

        return services;
    }
}