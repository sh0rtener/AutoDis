using AutoDis.Domain.Cars;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework.Repositories;

public sealed class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CarRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Car?> GetCar(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context
            .Cars.Include(c => c.Manufacturer)
            .ThenInclude(m => m.Country)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return entity == null ? null : _mapper.Map<Car>(entity);
    }

    public async Task<IEnumerable<CarCountry>> GetCountries(
        CancellationToken cancellationToken = default
    )
    {
        var entities = await _context.CarCountries.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CarCountry>>(entities);
    }

    public async Task<IEnumerable<CarManufacturer>> GetManufacturers(
        CancellationToken cancellationToken = default
    )
    {
        var entities = await _context
            .CarManufacturers.Include(m => m.Country)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<CarManufacturer>>(entities);
    }
}
