namespace Lifee.RSS.Application.Models.RssFeed;

public class RssFeedConfiguration
{
    public int Id { get; set; }
    
    public Guid Uuid { get; set; }
    
    public TimeSpan RefreshTime { get; set; }

    public int FeedId { get; set; }
}