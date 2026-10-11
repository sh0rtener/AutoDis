using MediatR;

namespace AutoDis.UseCases.Cars.GetCarModels;

public sealed class GetCarModelsQuery : IRequest<IEnumerable<string>>
{
    public required GetCarFilter Filter { get; set; }
}

public sealed class GetCarModelsQueryHandler
    : IRequestHandler<GetCarModelsQuery, IEnumerable<string>>
{
    private readonly IUseCaseCarRepository _carRepository;

    public GetCarModelsQueryHandler(IUseCaseCarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IEnumerable<string>> Handle(
        GetCarModelsQuery request,
        CancellationToken cancellationToken
    )
    {
        var carCountries = await _carRepository.GetModels(request.Filter, cancellationToken);
        return carCountries;
    }
}
