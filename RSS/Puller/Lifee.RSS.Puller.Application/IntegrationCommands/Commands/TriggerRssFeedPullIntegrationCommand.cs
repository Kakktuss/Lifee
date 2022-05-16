using HotBrokerBus.Abstractions.Commands;
using Lifee.Puller.RSS.IntegrationCommands.Results;

namespace Lifee.Puller.RSS.IntegrationCommands.Commands;

public class TriggerRssFeedPullIntegrationCommand : Command<TriggerRssFeedPullIntegrationCommandResult>
{
    
    public Guid RssFeedUuid { get; set; }
    
}