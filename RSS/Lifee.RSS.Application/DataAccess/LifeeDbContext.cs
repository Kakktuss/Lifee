using Lifee.RSS.Application.DataAccess.Configurations;
using Lifee.RSS.Application.Models.RssFeed;
using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Lifee.RSS.Application.DataAccess;

public class LifeeDbContext : DbContext, IUnitOfWork
{
    
    public LifeeDbContext(DbContextOptions<LifeeDbContext> options) : base(options)
    {
    }
    
    public DbSet<RssFeed> RssFeeds { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        modelBuilder.ApplyConfiguration(new RssFeedEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TagEntityConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRssFeedEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRssFeedTagEntityConfiguration());
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) > 0;
    }
}