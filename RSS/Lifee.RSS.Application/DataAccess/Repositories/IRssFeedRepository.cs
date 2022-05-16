using System.Linq.Expressions;
using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Application.DataAccess.Repositories;

public interface IRssFeedRepository : IDisposable
{
    public IUnitOfWork UnitOfWork { get; }
    
    public Task<RssFeed?> GetRssFeed(Expression<Func<RssFeed, bool>> where,
        Func<IQueryable<RssFeed>, IQueryable<RssFeed>>? func = null);

    public Task<bool> ExistsAsync(Expression<Func<RssFeed, bool>> where);

    public void UpdateRssFeed(RssFeed rssFeed);

    public void AddRssFeed(RssFeed rssFeed);
}