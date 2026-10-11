using AutoDis.UseCases.Cars;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework.Repositories;

public sealed class UseCaseCarBillRepository : IUseCaseCarBillRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UseCaseCarBillRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShortCarBillDto>> Get(
        GetCarBillsFilter filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context
            .CarBills.Include(b => b.Car)
            .ThenInclude(c => c.Manufacturer)
            .ThenInclude(m => m.Country)
            .Include(b => b.Notes)
            .AsNoTracking()
            .Include(b => b.Images)
            .AsQueryable();

        if (filter.CarId.HasValue)
        {
            query = query.Where(b => b.CarId == filter.CarId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.BillNoteName))
        {
            query = query.Where(b => b.Notes.Any(n => n.Name.Contains(filter.BillNoteName)));
        }

        return await query
            .OrderByDescending(b => b.Id)
            .Skip(filter.offset)
            .Take(filter.limit)
            .AsNoTracking()
            .ProjectTo<ShortCarBillDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetBillNotesByPrefix(
        string? billNoteNamePrefix,
        int limit = 100,
        int offset = 0,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.CarBillNotes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(billNoteNamePrefix))
        {
            query = query.Where(n => n.Name.StartsWith(billNoteNamePrefix));
        }

        return await query
            .Select(n => n.Name)
            .Distinct()
            .OrderBy(n => n)
            .Skip(offset)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
