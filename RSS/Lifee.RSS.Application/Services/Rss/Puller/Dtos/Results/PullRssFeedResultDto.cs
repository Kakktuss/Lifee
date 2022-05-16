using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Application.Services.Dtos.Results;

public class PullRssFeedResultDto
{
    public List<RssFeedItem>? RssFeedItems { get; set; }
}