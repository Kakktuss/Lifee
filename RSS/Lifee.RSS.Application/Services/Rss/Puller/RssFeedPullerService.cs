using FluentResults;
using HotBrokerBus.Abstractions.Stan.Commands;
using Lifee.RSS.Application.DataAccess.Repositories;
using Lifee.RSS.Application.IntegrationCommands.Commands;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;
using Microsoft.EntityFrameworkCore;

namespace Lifee.RSS.Application.Services;

public class RssFeedPullerService : IRssFeedPullerService
{

	private readonly IUserRepository _userRepository;

	private readonly IRssFeedRepository _rssFeedRepository;
	
	private readonly IStanCommandBusConsumerClient _stanCommandBusConsumerClient;

	public RssFeedPullerService(IUserRepository userRepository,
		IRssFeedRepository rssFeedRepository,
		IStanCommandBusConsumerClient stanCommandBusConsumerClient)
	{
		_userRepository = userRepository;

		_rssFeedRepository = rssFeedRepository;
		
		_stanCommandBusConsumerClient = stanCommandBusConsumerClient;
	}
	
	public async Task<Result<PullUserRssFeedResultDto>> PullUserFeed(PullUserRssFeedDto pullUserRssFeedDto)
	{
		var user = await _userRepository.GetUserAsync(e => e.Uuid == pullUserRssFeedDto.UserUuid, x => x.Include(e => e.RssFeeds).ThenInclude(e => e.RssFeed));

		if (user is null)
		{
			return Result.Fail(new Error("The user is not found").WithMetadata("errCode", "errUserNotFound"));
		}

		if(!user.RssFeeds.Any(e => e.RssFeed.Uuid.Equals(pullUserRssFeedDto.RssFeedUuid)))
		{
			if (await _rssFeedRepository.ExistsAsync(e => e.Uuid.Equals(pullUserRssFeedDto.RssFeedUuid)))
			{
				return Result.Fail(new Error("The RSS feed is not linked on this user").WithMetadata("errCode", "errRssFeedNotLinked"));
			}
			
			return Result.Fail(new Error("The RSS feed does not exists").WithMetadata("errCode", "errRssFeedNotFound"));
		}

		var pullResult = await PullFeed(new PullRssFeedDto
		{
			FeedUuid = pullUserRssFeedDto.RssFeedUuid
		});

		if (pullResult.IsFailed)
		{
			var error = new Error(pullResult.Reasons.FirstOrDefault()?.Message);

			foreach (var reason in pullResult.Reasons)
			{
				error.WithMetadata(reason.Message, reason.Metadata);
			}
			
			return Result.Fail(error);
		}

		return Result.Ok(new PullUserRssFeedResultDto
		{
			SynchronizedRssFeedItems = pullResult.ValueOrDefault.RssFeedItems
		});
	}
	
	public async Task<Result<PullRssFeedResultDto>> PullFeed(PullRssFeedDto pullRssFeedDto)
	{
		var result = await _stanCommandBusConsumerClient.SendAsync("rss.feed.pull.trigger", new TriggerRssFeedPullIntegrationCommand
		{
			RssFeedUuid = pullRssFeedDto.FeedUuid
		});

		if (result is null)
		{
			return Result.Fail(new Error("An error happened while trying to pull the feed").WithMetadata("errCode", "errUnableToPullFeed"));
		}

		if (!result.IsSuccess)
		{
			var error = new Error(result.Reasons.FirstOrDefault().Item1);

			foreach (var reason in result.Reasons)
			{
				error.WithMetadata(reason.Item1, reason.Item2);
			}

			return Result.Fail(error);
		}

		return Result.Ok(new PullRssFeedResultDto
		{
			RssFeedItems = result.SynchronizedItems
		});
	}
	
}