using FluentResults;
using Lifee.Application.Services.Dtos;

namespace Lifee.Application.Services;

public class PocketSynchronizer : IPocketSynchronizerService
{
	
	public Result SynchronizeUserRssFeeds(SynchronizeUserFeedsDto synchronizeUserFeedsDto)
	{
		return Result.Ok();
	}
	
	public Result SynchronizeUserRssFeed(SynchronizeUserRssFeedDto synchronizeUserRssFeedDto)
	{
		return Result.Ok();
	}

}