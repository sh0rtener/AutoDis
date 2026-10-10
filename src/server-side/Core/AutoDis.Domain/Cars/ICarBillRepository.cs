namespace AutoDis.Domain.Cars;

public interface ICarBillRepository
{
    public Task<CarBill> GetBill(int id, CancellationToken cancellationToken = default);
    public Task AddAsync(CarBill bill, CancellationToken cancellationToken = default);
    public Task UpdateBill(int id, CarBill carBill, CancellationToken cancellationToken = default);
}
