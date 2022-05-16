using HotBrokerBus.Abstractions.Events;
using Lifee.Pocket.Synchronizer.Application.IntegrationEvents.Events;
using Lifee.Pocket.Synchronizer.Application.Service;
using PocketSharp;

namespace Lifee.Pocket.Synchronizer.Application.IntegrationEvents.Handlers;

public class UserCreatedIntegrationEventHandler : IEventHandler<UserCreatedIntegrationEvent>
{
	private readonly IPocketService _pocketService;
	
	public UserCreatedIntegrationEventHandler(IPocketService pocketService)
	{
		
	}
	
	public Task<bool> Handle(UserCreatedIntegrationEvent @event)
	{
		throw new NotImplementedException();
	}
}