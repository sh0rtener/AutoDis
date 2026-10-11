using AutoDis.UseCases.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDis.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IResourceSaver, FileserverSaver>();

        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        );

        return services;
    }
}
