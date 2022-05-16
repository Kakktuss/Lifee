using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Pocket.Application.DataAccess.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasMany(e => e.RssFeedTags)
			.WithOne(e => e.User)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(e => e.PocketTags)
			.WithOne(e => e.User)
			.OnDelete(DeleteBehavior.Cascade);
	}
}