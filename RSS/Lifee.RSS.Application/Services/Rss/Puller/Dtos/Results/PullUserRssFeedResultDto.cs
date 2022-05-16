using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Application.Services.Dtos.Results;

public class PullUserRssFeedResultDto
{
	public List<RssFeedItem>? SynchronizedRssFeedItems { get; init; }
}