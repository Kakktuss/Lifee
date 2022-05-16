using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Application.Models.Tag;

public class Category
{

    public Category()
    {
       Uuid = Guid.NewGuid(); 
       
       RssFeeds = new List<RssFeed.RssFeed>();
    }
    
    public int Id { get; init; }
    
    public Guid Uuid { get; }
    
    public List<RssFeed.RssFeed> RssFeeds { get; }

}