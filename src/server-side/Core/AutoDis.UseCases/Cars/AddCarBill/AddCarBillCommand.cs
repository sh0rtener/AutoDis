using AutoDis.Domain;
using AutoDis.Domain.Cars;
using AutoDis.UseCases.Resources;
using MediatR;

namespace AutoDis.UseCases.Cars.AddCarBill;

public sealed class AddCarBillCommand : IRequest
{
    public int CarId { get; set; }
    public IEnumerable<ShortCarBillNoteDto> Notes { get; set; } = new List<ShortCarBillNoteDto>();
    public decimal Cost { get; set; }
    public IEnumerable<FileData> Images { get; set; } = new List<FileData>();
}

public sealed class AddCarBillCommandHandler : IRequestHandler<AddCarBillCommand>
{
    private readonly ICarBillRepository _carBillRepository;
    private readonly ICarRepository _carRepository;
    private readonly IResourceSaver _resourceSaver;
    private readonly IUnitOfWork _unitOfWork;

    public AddCarBillCommandHandler(
        ICarBillRepository carBillRepository,
        ICarRepository carRepository,
        IResourceSaver resourceSaver,
        IUnitOfWork unitOfWork
    )
    {
        _carBillRepository = carBillRepository;
        _carRepository = carRepository;
        _resourceSaver = resourceSaver;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddCarBillCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var car =
                await _carRepository.GetCar(request.CarId, cancellationToken)
                ?? throw new ArgumentNullException("Car wasn't found");

            var notes = request.Notes.Select(x => new CarBillNote(
                x.Name ?? throw new ArgumentNullException("CarBillNoteName"),
                x.Cost,
                x.IsAvailableForSale
            ));

            var carBill = new CarBill(car);
            foreach (var item in notes)
                carBill.AddNote(item);

            foreach (var item in request.Images)
            {
                await _resourceSaver.SaveResource(
                    await StreamToBytesAsync(item.Content),
                    item.ContentType,
                    cancellationToken
                );
            }

            await _carBillRepository.AddAsync(carBill, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task<byte[]> StreamToBytesAsync(Stream stream)
    {
        using MemoryStream memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
