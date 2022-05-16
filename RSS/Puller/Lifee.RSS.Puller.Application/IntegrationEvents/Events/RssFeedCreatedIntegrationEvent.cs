using HotBrokerBus.Abstractions.Events;

namespace Lifee.Puller.RSS.IntegrationEvents.Events;

public class RssFeedCreatedIntegrationEvent : Event
{
    public Guid RssFeedUuid { get; set; }
}