using Lifee.Puller.RSS.Models.RssFeed;

namespace Lifee.Puller.RSS.Service.Dtos.Results;

public class PullRssFeedResultDto
{
	public List<RssFeedItem> SynchronizedItems { get; init; }
}