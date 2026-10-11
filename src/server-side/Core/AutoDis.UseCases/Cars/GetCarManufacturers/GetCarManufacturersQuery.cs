using AutoDis.Domain.Cars;
using MediatR;

namespace AutoDis.UseCases.Cars.GetCarManufacturers;

public sealed class GetCarManufacturersQuery : IRequest<IEnumerable<string>> { }

public sealed class GetCarManufacturersQueryHandler
    : IRequestHandler<GetCarManufacturersQuery, IEnumerable<string>>
{
    private readonly ICarRepository _carRepository;

    public GetCarManufacturersQueryHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IEnumerable<string>> Handle(
        GetCarManufacturersQuery request,
        CancellationToken cancellationToken
    )
    {
        var carCountries = await _carRepository.GetManufacturers(cancellationToken);
        return carCountries.Select(x => x.Name);
    }
}
