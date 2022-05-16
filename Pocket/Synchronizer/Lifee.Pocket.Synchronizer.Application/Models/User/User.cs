namespace Lifee.RSS.Application.Models.User;

public class User
{
	public int Id { get; init; }
	
	public string Uuid { get; init; }
	
	public UserPocketCredentials PocketCredentials { get; init; }
	
	public List<UserPocketTag> PocketTags { get; init; }
	
	public List<UserPocketArticle> Articles { get; init; }
}