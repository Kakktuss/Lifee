using HotBrokerBus.Abstractions.Events;
using Lifee.RSS.Pocket.Application.IntegrationEvents.Events.RssFeed;

namespace Lifee.RSS.Pocket.Application.IntegrationEvents.Handlers;

public class RssFeedSynchronizedIntegrationEventHandler : IEventHandler<RssFeedSynchronizedIntegrationEvent>
{
    public Task<bool> Handle(RssFeedSynchronizedIntegrationEvent @event)
    {
        throw new NotImplementedException();
    }
}