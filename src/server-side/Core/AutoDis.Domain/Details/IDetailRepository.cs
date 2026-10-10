namespace AutoDis.Domain.Details;

public interface IDetailRepository
{
    public Task<Detail> GetDetail(Guid id, CancellationToken cancellationToken = default);
    public Task AddDetail(Detail detail, CancellationToken cancellationToken = default);
    public Task UpdateDetail(int id, Detail detail, CancellationToken cancellationToken = default);
}