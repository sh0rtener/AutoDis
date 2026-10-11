using AutoDis.UseCases.Cars;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework.Repositories;

public sealed class UseCaseCarRepository : IUseCaseCarRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UseCaseCarRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShortCarDto>> Get(
        GetCarFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context
            .Cars.Include(c => c.Manufacturer)
            .ThenInclude(m => m.Country)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            query = query.Where(c => c.Model.Contains(filter.Model));
        }

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer.Name.Contains(filter.Manufacturer));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            query = query.Where(c => c.Manufacturer.Country.Name.Contains(filter.Country));
        }

        if (filter.Year.HasValue)
        {
            query = query.Where(c => c.Year == filter.Year.Value);
        }

        return await query
            .OrderBy(c => c.Id)
            .ProjectTo<ShortCarDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetModels(
        GetCarFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context
            .Cars.Include(c => c.Manufacturer)
            .ThenInclude(m => m.Country)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            query = query.Where(c => c.Model.Contains(filter.Model));
        }

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer.Name.Contains(filter.Manufacturer));
        }

        if (!string.IsNullOrWhiteSpace(filter.Country))
        {
            query = query.Where(c => c.Manufacturer.Country.Name.Contains(filter.Country));
        }

        if (filter.Year.HasValue)
        {
            query = query.Where(c => c.Year == filter.Year.Value);
        }

        return await query
            .Select(c => c.Model)
            .Distinct()
            .OrderBy(m => m)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
