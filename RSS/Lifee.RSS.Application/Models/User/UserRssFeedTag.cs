namespace Lifee.RSS.Application.Models.User;

public class UserRssFeedTag
{
    public UserRssFeedTag()
    {
        Uuid = Guid.NewGuid();
    }
    
    public int Id { get; init; }

    public Guid Uuid { get; }

    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<UserRssFeed> RssFeeds { get; set; }
    
    public int UserId { get; }
    
    public User User { get; }

}