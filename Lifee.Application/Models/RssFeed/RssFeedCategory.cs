namespace Lifee.Application.Models.RssFeed;

public class RssFeedCategory
{
	
	public Guid Uuid { get; set; }
	
	public string Name { get; set; }
	
	public TimeSpan RefreshTime { get; set; }

}