using FluentResults;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;

namespace Lifee.RSS.Application.Services;

public interface IRssFeedPullerService
{
	public Task<Result<PullUserRssFeedResultDto>> PullUserFeed(PullUserRssFeedDto pullUserRssFeedDto);

	public Task<Result<PullRssFeedResultDto>> PullFeed(PullRssFeedDto pullRssFeedDto);
}