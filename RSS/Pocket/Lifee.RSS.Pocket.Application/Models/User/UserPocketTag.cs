namespace Lifee.RSS.Application.Models.User;

public class UserPocketTag
{
	public UserPocketTag(Guid uuid)
	{
		Uuid = uuid;
		
		RssFeedTags = new List<UserRssFeedTag>();
	}
	
	public int Id { get; }
	
	public Guid Uuid { get; }
	
	public List<UserRssFeedTag> RssFeedTags { get; }

	public int UserId { get; }
    
	public User User { get; }
}