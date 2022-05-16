namespace Lifee.Application.Services.Dtos;

public class SynchronizeUserRssFeedDto
{
	public Guid UserUuid { get; set; }
	
	public Guid RssFeedUuid { get; set; }
}