namespace Lifee.Application.Models.RssFeed;

public class RssFeed
{
	public Guid Uuid { get; set; }
	
	public string Url { get; set; }
	
	public string Title { get; set; }
	
	public string Description { get; set; }
	
	public TimeSpan RefreshTime { get; set; }
	
	public RssFeedCategory Category { get; set; }

	public List<RssFeedItem> Contents { get; set; }
}