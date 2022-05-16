using HotBrokerBus.Abstractions.Commands;
using Lifee.Pocket.Synchronizer.Application.IntegrationCommands.Results;

namespace Lifee.Pocket.Synchronizer.Application.IntegrationCommands.Handlers;

public class CreatePocketArticleIntegrationCommand : Command<CreatePocketArticleIntegrationCommandResult>
{
	
	public string Title { get; set; }
	
}