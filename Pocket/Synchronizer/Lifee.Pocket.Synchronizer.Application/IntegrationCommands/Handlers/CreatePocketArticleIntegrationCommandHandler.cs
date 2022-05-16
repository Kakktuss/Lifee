using HotBrokerBus.Abstractions.Commands;
using Lifee.Pocket.Synchronizer.Application.IntegrationCommands.Results;

namespace Lifee.Pocket.Synchronizer.Application.IntegrationCommands.Handlers;

public class CreatePocketArticleIntegrationCommandHandler : ICommandHandler<CreatePocketArticleIntegrationCommand, CreatePocketArticleIntegrationCommandResult>
{
	public CreatePocketArticleIntegrationCommandHandler()
	{
		
	}
	
	public Task<object> Process(CreatePocketArticleIntegrationCommand command)
	{
		throw new NotImplementedException();
	}
}