namespace AutoDis.UseCases.Cars;

public interface ICarSeedService
{
    Task LoadSeeds(CancellationToken cancellationToken = default);
}
