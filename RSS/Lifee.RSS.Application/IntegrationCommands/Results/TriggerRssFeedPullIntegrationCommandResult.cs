using HotBrokerBus.Abstractions.Commands;
using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Application.IntegrationCommands.Results;

public class TriggerRssFeedPullIntegrationCommandResult : CommandResult
{
	public bool IsSuccess { get; init; }
	public List<RssFeedItem>? SynchronizedItems { get; init; }
	public List<(string, Dictionary<string, object>)> Reasons { get; init; }
}