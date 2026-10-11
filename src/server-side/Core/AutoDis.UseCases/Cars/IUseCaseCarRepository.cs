namespace AutoDis.UseCases.Cars;

public record GetCarFilter(string? Model, string? Manufacturer, string? Country, int? Year);

public interface IUseCaseCarRepository
{
    public Task<IEnumerable<ShortCarDto>> Get(
        GetCarFilter filter,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<string>> GetModels(
        GetCarFilter filter,
        CancellationToken cancellationToken = default
    );
}
