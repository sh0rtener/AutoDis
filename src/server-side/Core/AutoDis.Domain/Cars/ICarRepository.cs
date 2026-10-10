namespace AutoDis.Domain.Cars;

public interface ICarRepository
{
    public Task<Car> GetCar(int id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<CarManufacturer>> GetManufacturers(
        CancellationToken cancellationToken = default
    );
    public Task<IEnumerable<CarCountry>> GetCountries(
        CancellationToken cancellationToken = default
    );
}
