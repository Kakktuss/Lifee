using HotBrokerBus.Abstractions.Commands;
using Lifee.Puller.RSS.Models.RssFeed;

namespace Lifee.Puller.RSS.IntegrationCommands.Results;

public class TriggerRssFeedPullIntegrationCommandResult : CommandResult
{
	public bool IsSuccess { get; init; }
	public List<RssFeedItem>? SynchronizedItems { get; init; }
	public List<(string, Dictionary<string, object>)> Reasons { get; init; }
}