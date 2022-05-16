using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Application.DataAccess.Configurations;

public class UserRssFeedTagEntityConfiguration : IEntityTypeConfiguration<UserRssFeedTag>
{
	public void Configure(EntityTypeBuilder<UserRssFeedTag> builder)
	{
		builder.ToTable("UserRssFeedTags");

		builder.HasKey(e => e.Id);
        
		builder.Property(e => e.Id)
			.UseIdentityColumn()
			.Metadata
			.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

		builder.Property(e => e.Uuid)
			.IsRequired();

		builder.HasMany(e => e.RssFeeds)
			.WithMany(e => e.Tags)
			.UsingEntity<Dictionary<string, object?>>(
				j => j.HasOne<UserRssFeed>().WithMany().OnDelete(DeleteBehavior.Restrict),
				j => j.HasOne<UserRssFeedTag>().WithMany().OnDelete(DeleteBehavior.Restrict));
	}
}