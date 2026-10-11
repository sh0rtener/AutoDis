using AutoDis.Domain.Cars;
using AutoDis.Persistense.EntityFramework.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoDis.Persistense.EntityFramework.Repositories;

public sealed class CarBillRepository : ICarBillRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CarBillRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(CarBill bill, CancellationToken cancellationToken = default)
    {
        var carEntity = await _context.Cars.FirstOrDefaultAsync(
            c => c.Id == bill.Car.Id,
            cancellationToken
        );

        if (carEntity == null)
            throw new InvalidOperationException($"Car with id {bill.Car.Id} not found");

        var entity = _mapper.Map<CarBillEntity>(bill);
        entity.CarId = carEntity.Id;

        _context.CarBills.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<CarBill?> GetBill(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _context
            .CarBills.Include(b => b.Car)
            .ThenInclude(c => c.Manufacturer)
            .ThenInclude(m => m.Country)
            .Include(b => b.Notes)
            .Include(b => b.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        return entity == null ? null : _mapper.Map<CarBill>(entity);
    }

    public async Task UpdateBill(int id, CarBill carBill, CancellationToken cancellationToken = default)
    {
        var entity = await _context
            .CarBills.Include(b => b.Notes)
            .Include(b => b.Images)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (entity == null)
            throw new InvalidOperationException($"CarBill with id {id} not found");

        entity.Cost = carBill.Cost;
        entity.IsArchieve = carBill.IsArchieve;

        var existingNotes = entity.Notes.ToDictionary(n => n.Id);
        foreach (var note in carBill.Notes)
        {
            if (existingNotes.TryGetValue(note.Id, out var existingNote))
            {
                existingNote.Name = note.Name;
                existingNote.Cost = note.Cost;
                existingNote.Available = note.IsAvailableForSale;
                existingNotes.Remove(note.Id);
            }
            else
            {
                var noteEntity = _mapper.Map<CarBillNoteEntity>(note);
                noteEntity.Id = note.Id;
                noteEntity.CarBillId = id;
                entity.Notes.Add(noteEntity);
            }
        }

        foreach (var noteToRemove in existingNotes.Values)
        {
            entity.Notes.Remove(noteToRemove);
        }

        var existingImages = entity
            .Images.Select(i => i.ImagePath)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var newImages = carBill.ImagePaths.ToHashSet(StringComparer.OrdinalIgnoreCase);

        var imagesToRemove = entity.Images.Where(i => !newImages.Contains(i.ImagePath)).ToList();
        foreach (var image in imagesToRemove)
        {
            entity.Images.Remove(image);
        }

        foreach (var imagePath in carBill.ImagePaths)
        {
            if (!existingImages.Contains(imagePath))
            {
                var imageEntity = _mapper.Map<CarBillImageEntity>(imagePath);
                imageEntity.CarBillId = id;
                entity.Images.Add(imageEntity);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
