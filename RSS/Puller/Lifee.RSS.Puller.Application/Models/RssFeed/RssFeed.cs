namespace Lifee.Puller.RSS.Models.RssFeed;

public class RssFeed
{
	public RssFeed()
	{
		Uuid = Guid.NewGuid();

		Items = new List<RssFeedItem>();
	}
	
	public int Id { get; }
	
	public Guid Uuid { get; }
	
	public string Url { get; init; }
	
	public string Title { get; init; }
	
	public string Description { get; init; }
	
	public RssFeedConfiguration Configuration { get; init; }

	public List<RssFeedItem> Items { get; init; }

	public void AddItem(RssFeedItem item)
	{
		Items.Add(item);
	}

	public void AddItems(List<RssFeedItem> items)
	{
		Items.AddRange(items);
	}
	
	public void RemoveItem(RssFeedItem item)
	{
		Items.Remove(item);
	}
}