namespace Lifee.RSS.Application.Models.User;

public class UserRssFeed
{
	public UserRssFeed()
	{
		Tags = new List<UserRssFeedTag>();
	}
	
	public int Id { get; }

	public List<UserRssFeedTag> Tags { get; }
	
	public User User { get; init; }
	
	public int UserId { get; }

	public RssFeed.RssFeed RssFeed { get; init; }
	
	public int RssFeedId { get; }

}