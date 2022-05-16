using FluentResults;
using Lifee.Puller.RSS.Service.Dtos;
using Lifee.Puller.RSS.Service.Dtos.Results;

namespace Lifee.Puller.RSS.Service;

public interface IRssFeedService : IDisposable
{
    public Task<Result<PullRssFeedResultDto>> PullFeed(PullRssFeedDto pullRssFeedDto);
}