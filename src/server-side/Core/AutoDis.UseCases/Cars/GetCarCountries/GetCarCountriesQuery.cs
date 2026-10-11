using AutoDis.Domain.Cars;
using MediatR;

namespace AutoDis.UseCases.Cars.GetCarCountries;

public sealed class GetCarCountriesQuery : IRequest<IEnumerable<string>> { }

public sealed class GetCarCountriesQueryHandler
    : IRequestHandler<GetCarCountriesQuery, IEnumerable<string>>
{
    private readonly ICarRepository _carRepository;

    public GetCarCountriesQueryHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IEnumerable<string>> Handle(
        GetCarCountriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var carCountries = await _carRepository.GetCountries(cancellationToken);
        return carCountries.Select(x => x.Name);
    }
}
