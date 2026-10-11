namespace AutoDis.UseCases.Resources;

public interface IResourceSaver
{
    public Task<string> SaveResource(byte[] resource, string type, CancellationToken cancellationToken = default);
}
