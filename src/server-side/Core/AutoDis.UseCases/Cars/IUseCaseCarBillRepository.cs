namespace AutoDis.UseCases.Cars;

public record GetCarBillsFilter(int? CarId, string? BillNoteName, int limit = 100, int offset = 0);

public interface IUseCaseCarBillRepository
{
    public Task<IEnumerable<ShortCarBillDto>> Get(
        GetCarBillsFilter filter,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<string>> GetBillNotesByPrefix(
        string? billNoteNamePrefix,
        int limit = 100,
        int offset = 0,
        CancellationToken cancellationToken = default
    );
}
