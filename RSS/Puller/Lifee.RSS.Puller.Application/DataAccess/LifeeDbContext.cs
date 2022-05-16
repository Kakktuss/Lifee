using Lifee.Puller.RSS.DataAccess.Configurations;
using Lifee.Puller.RSS.Models.RssFeed;
using Microsoft.EntityFrameworkCore;

namespace Lifee.Puller.RSS.DataAccess;

public class LifeeDbContext : DbContext, IUnitOfWork
{
    
    public LifeeDbContext(DbContextOptions<LifeeDbContext> options) : base(options)
    {
    }
    
    public DbSet<RssFeed> RssFeeds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RssFeedEntityConfiguration());
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) > 0;
    }
}