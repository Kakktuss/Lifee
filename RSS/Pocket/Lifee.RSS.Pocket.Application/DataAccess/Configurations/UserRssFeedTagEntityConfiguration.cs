using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Pocket.Application.DataAccess.Configurations;

public class UserRssFeedTagEntityConfiguration : IEntityTypeConfiguration<UserRssFeedTag>
{
	public void Configure(EntityTypeBuilder<UserRssFeedTag> builder)
	{
		builder.ToTable("UserRssFeedTags");

		builder.HasMany(e => e.PocketTags)
			.WithMany(e => e.RssFeedTags)
			.UsingEntity<Dictionary<string, object>>(
				"UserRssPocketTags",
				j => j.HasOne<UserPocketTag>().WithMany().OnDelete(DeleteBehavior.Restrict),
				j => j.HasOne<UserRssFeedTag>().WithMany().OnDelete(DeleteBehavior.Restrict));
	}
}