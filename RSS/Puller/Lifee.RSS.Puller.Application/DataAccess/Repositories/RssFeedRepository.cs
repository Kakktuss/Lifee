using System.Linq.Expressions;
using Lifee.Puller.RSS.Models.RssFeed;
using Microsoft.EntityFrameworkCore;

namespace Lifee.Puller.RSS.DataAccess.Repositories;

public class RssFeedRepository : IRssFeedRepository
{

    private readonly LifeeDbContext _context;

    private readonly DbSet<RssFeed> _rssFeeds;
    
    public RssFeedRepository(LifeeDbContext context)
    {
        _context = context;
        
        _rssFeeds = context.Set<RssFeed>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public Task<RssFeed?> GetRssFeed(Expression<Func<RssFeed, bool>> where, Func<IQueryable<RssFeed>, IQueryable<RssFeed>>? func = null)
    {
        if(_disposed)
            throw new ObjectDisposedException(nameof(RssFeedRepository));
        
        if (func is not null)
            return func(_rssFeeds).FirstOrDefaultAsync(where);

        return _rssFeeds.FirstOrDefaultAsync(where);
    }
    
    public Task<List<RssFeed>> GetRssFeeds(Expression<Func<RssFeed, bool>> where, Func<IQueryable<RssFeed>, IQueryable<RssFeed>>? func = null)
    {
        if(_disposed)
            throw new ObjectDisposedException(nameof(RssFeedRepository));
            
        if (func is not null)
            return func(_rssFeeds).Where(where).ToListAsync();
        
        return _rssFeeds.Where(where).ToListAsync();
    }
    
    public void UpdateRssFeed(RssFeed rssFeed)
    {
        _rssFeeds.Update(rssFeed);
    }

    private bool _disposed = false;

    protected void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}