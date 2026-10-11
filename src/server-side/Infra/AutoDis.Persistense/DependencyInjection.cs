using System.Reflection;
using AutoDis.Domain;
using AutoDis.Domain.Cars;
using AutoDis.Persistense.EntityFramework;
using AutoDis.Persistense.EntityFramework.Repositories;
using AutoDis.UseCases.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDis.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(c =>
        {
            c.AddMaps(Assembly.GetExecutingAssembly());
        });

        var connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidDataException("Connection string is empty");

        services.AddDbContext<AppDbContext>(x => x.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICarBillRepository, CarBillRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUseCaseCarBillRepository, UseCaseCarBillRepository>();
        services.AddScoped<IUseCaseCarRepository, UseCaseCarRepository>();

        return services;
    }
}
