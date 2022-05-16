using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lifee.RSS.Pocket.Application.DataAccess.Configurations;

public class UserPocketTagEntityConfiguration : IEntityTypeConfiguration<UserPocketTag>
{
	public void Configure(EntityTypeBuilder<UserPocketTag> builder)
	{
		throw new NotImplementedException();
	}
}