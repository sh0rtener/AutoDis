using AutoDis.UseCases.Cars;

namespace AutoDis.Persistense.EntityFramework;

public sealed class CarSeedService : ICarSeedService
{
    public Task LoadSeeds(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}