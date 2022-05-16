using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Application.DataAccess.Configurations;

public class UserRssFeedEntityConfiguration : IEntityTypeConfiguration<UserRssFeed>
{
	public void Configure(EntityTypeBuilder<UserRssFeed> builder)
	{
		builder.ToTable("UserRssFeeds");
		
		builder.HasKey(e => e.Id);
        
		builder.Property(e => e.Id)
			.UseIdentityColumn()
			.Metadata
			.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

		builder.HasOne(e => e.RssFeed)
			.WithMany(e => e.Users)
			.HasForeignKey(e => e.RssFeedId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}