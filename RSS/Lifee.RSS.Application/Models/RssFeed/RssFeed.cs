using Lifee.RSS.Application.Models.Tag;
using Lifee.RSS.Application.Models.User;

namespace Lifee.RSS.Application.Models.RssFeed;

public class RssFeed
{
	public RssFeed()
	{
		Uuid = Guid.NewGuid();

		Users = new List<UserRssFeed>();
		
		Categories = new List<Category>();

		Items = new List<RssFeedItem>();
	}
	
	public int Id { get; }
	
	public Guid Uuid { get; }
	
	public string Url { get; init; }
	
	public string Title { get; init; }
	
	public string Description { get; init; }
	
	public RssFeedConfiguration Configuration { get; init; }
	
	public List<UserRssFeed> Users { get; init; }

	public List<RssFeedItem> Items { get; init; }
	
	public List<Category> Categories { get; init; }

	public void AddItem(RssFeedItem item)
	{
		Items.Add(item);
	}
	
	public void RemoveItem(RssFeedItem item)
	{
		Items.Remove(item);
	}
}