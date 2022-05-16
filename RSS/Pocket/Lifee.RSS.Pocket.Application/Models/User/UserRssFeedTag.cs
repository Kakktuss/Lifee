namespace Lifee.RSS.Application.Models.User;

public class UserRssFeedTag
{
    public UserRssFeedTag(Guid uuid)
    {
        Uuid = uuid;
        
        PocketTags = new List<UserPocketTag>();
    }
    
    public int Id { get; init; }

    public Guid Uuid { get; }
    
    public List<UserPocketTag> PocketTags { get; }

    public int UserId { get; }
    
    public User User { get; }

}