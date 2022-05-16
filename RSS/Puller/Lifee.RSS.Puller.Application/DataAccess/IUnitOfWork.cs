namespace Lifee.Puller.RSS.DataAccess;

public interface IUnitOfWork
{
    public Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
}