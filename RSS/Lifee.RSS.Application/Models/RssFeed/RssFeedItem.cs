namespace Lifee.RSS.Application.Models.RssFeed;

public class RssFeedItem
{
	public RssFeedItem()
    {
    	Uuid = Guid.NewGuid();
    }
		
	public int Id { get; set; }
	
	public Guid Uuid { get; set; }
	
	public string ItemId { get; set; }
	
	public string Title { get; set; }
	
	public string Description { get; set; }
	
	public string Link { get; set; }

	public int FeedId { get; set; }
}