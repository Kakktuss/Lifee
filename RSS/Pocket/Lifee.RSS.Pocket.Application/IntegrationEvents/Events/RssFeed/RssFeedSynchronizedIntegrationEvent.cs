using HotBrokerBus.Abstractions.Events;

namespace Lifee.RSS.Pocket.Application.IntegrationEvents.Events.RssFeed;

public class RssFeedSynchronizedIntegrationEvent : Event
{
    
    public Guid RssFeedUuid { get; set; }
    
    
    
}