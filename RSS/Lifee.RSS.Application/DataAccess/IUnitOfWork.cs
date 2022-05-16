namespace Lifee.RSS.Application.DataAccess;

public interface IUnitOfWork
{
    public Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
}