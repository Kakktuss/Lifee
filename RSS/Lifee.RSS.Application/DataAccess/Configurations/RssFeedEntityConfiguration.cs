using Lifee.RSS.Application.Models.RssFeed;
using Lifee.RSS.Application.Models.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Application.DataAccess.Configurations;

public class RssFeedEntityConfiguration : IEntityTypeConfiguration<RssFeed>
{
    public void Configure(EntityTypeBuilder<RssFeed> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .UseIdentityColumn()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(e => e.Uuid)
            .IsRequired();

        builder.Property(e => e.Title)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired();

        builder.HasMany(e => e.Users)
            .WithOne(e => e.RssFeed)
            .HasForeignKey(e => e.RssFeedId);

        builder.OwnsOne(e => e.Configuration, navigationBuilder =>
        {
            navigationBuilder.ToTable("RssFeedConfigurations");

            navigationBuilder.WithOwner()
                .HasForeignKey(e => e.FeedId);
            
            navigationBuilder.HasKey(e => e.Id);

            navigationBuilder.Property(e => e.Id)
                .UseIdentityColumn();

            navigationBuilder.Property(e => e.Uuid)
                .IsRequired();
            
            navigationBuilder.Property(e => e.RefreshTime)
                .IsRequired();
        });
        
        builder.OwnsMany(e => e.Items, navigationBuilder =>
        {
            navigationBuilder.ToTable("RssFeedItems");
            
            navigationBuilder.WithOwner()
                .HasForeignKey(e => e.FeedId);

            navigationBuilder.HasKey(e => e.Id);

            navigationBuilder.Property(e => e.Id)
                .UseIdentityColumn();

            navigationBuilder.Property(e => e.Uuid)
                .IsRequired();
            
            navigationBuilder.Property(e => e.ItemId)
                .IsRequired();

            navigationBuilder.Property(e => e.Title)
                .IsRequired();
            
            navigationBuilder.Property(e => e.Description)
                .IsRequired();

            navigationBuilder.Property(e => e.Link)
                .IsRequired();
            
            navigationBuilder.Property(e => e.FeedId);
        });
        
        builder.HasMany(e => e.Categories)
            .WithMany(e => e.RssFeeds)
            .UsingEntity<Dictionary<string, object?>>(
                j => j.HasOne<Category>().WithMany().OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<RssFeed>().WithMany().OnDelete(DeleteBehavior.Restrict));
    }
}