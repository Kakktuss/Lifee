using HotBrokerBus.Abstractions.Events;

namespace Lifee.Pocket.Synchronizer.Application.IntegrationEvents.Events;

public class UserCreatedIntegrationEvent : Event
{
	
	public string Uuid { get; set; }

	
}