using HotBrokerBus.Abstractions.Commands;
using Lifee.RSS.Application.IntegrationCommands.Results;

namespace Lifee.RSS.Application.IntegrationCommands.Commands;

public class TriggerRssFeedPullIntegrationCommand : Command<TriggerRssFeedPullIntegrationCommandResult>
{
    
    public Guid RssFeedUuid { get; set; }
    
}