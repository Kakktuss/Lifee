namespace Lifee.RSS.Application.Models.User;

public class User
{
	public User()
	{
		RssFeeds = new List<UserRssFeed>();

		RssFeedTags = new List<UserRssFeedTag>();
	}
	
	public int Id { get; }
	
	public string Uuid { get; init; }
	
	public List<UserRssFeed> RssFeeds { get; }
	
	public List<UserRssFeedTag> RssFeedTags { get; }

	public void AddRssFeed(RssFeed.RssFeed rssFeed)
	{
		if (rssFeed is null)
		{
			throw new ArgumentNullException("RssFeed is null");
		}
		
		RssFeeds.Add(new UserRssFeed
		{
			User = this,
			RssFeed = rssFeed
		});
	}
}