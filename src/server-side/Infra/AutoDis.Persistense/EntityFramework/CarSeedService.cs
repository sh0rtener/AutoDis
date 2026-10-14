using AutoDis.Persistense.EntityFramework.Entities;
using AutoDis.UseCases.Cars;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework;

public record CountrySeed(int id, string IsoCode, string Name);

public record ManufacturerSeed(int id, string Name, int CountryId);

public sealed class CarSeedService : ICarSeedService
{
    private readonly AppDbContext _context;

    public CarSeedService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadSeeds(CancellationToken cancellationToken = default)
    {
        var countries = SeedParser.ParseCountries("CountriesSeed.csv", ':').ToList();
        var manufacturers = SeedParser.ParseManufacturers("ManufacturersSeed.csv", ':').ToList();

        if (!await _context.CarCountries.AnyAsync(cancellationToken))
            await _context.CarCountries.AddRangeAsync(
                countries.Select(x => new CarCountryEntity()
                {
                    Id = x.id,
                    IsoCode = x.IsoCode,
                    Name = x.Name,
                }),
                cancellationToken
            );

        await _context.SaveChangesAsync(cancellationToken);

        if (!await _context.CarManufacturers.AnyAsync(cancellationToken))
            await _context.CarManufacturers.AddRangeAsync(
                manufacturers.Select(x => new CarManufacturerEntity()
                {
                    Id = x.id,
                    Name = x.Name,
                    CountryId = x.CountryId,
                })
            );

        await _context.SaveChangesAsync(cancellationToken);
    }
}
