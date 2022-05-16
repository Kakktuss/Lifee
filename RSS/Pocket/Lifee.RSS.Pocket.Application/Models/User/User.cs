namespace Lifee.RSS.Application.Models.User;

public class User
{
	public User()
	{
		RssFeedTags = new List<UserRssFeedTag>();
	}
	
	public int Id { get; }
	
	public string Uuid { get; init; }

	public List<UserRssFeedTag> RssFeedTags { get; }
	
	public List<UserPocketTag> PocketTags { get; }
}