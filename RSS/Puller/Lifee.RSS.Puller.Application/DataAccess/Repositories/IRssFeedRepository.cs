using System.Linq.Expressions;
using Lifee.Puller.RSS.Models.RssFeed;

namespace Lifee.Puller.RSS.DataAccess.Repositories;

public interface IRssFeedRepository : IDisposable
{
    public IUnitOfWork UnitOfWork { get; }
    
    public Task<RssFeed?> GetRssFeed(Expression<Func<RssFeed, bool>> where,
        Func<IQueryable<RssFeed>, IQueryable<RssFeed>>? func = null);

    public void UpdateRssFeed(RssFeed rssFeed);
}