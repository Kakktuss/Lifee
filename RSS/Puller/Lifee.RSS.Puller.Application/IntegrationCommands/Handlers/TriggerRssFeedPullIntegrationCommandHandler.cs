using Hangfire;
using HotBrokerBus.Abstractions.Commands;
using Lifee.Puller.RSS.IntegrationCommands.Commands;
using Lifee.Puller.RSS.IntegrationCommands.Results;
using Lifee.Puller.RSS.Jobs;
using Lifee.Puller.RSS.Service;
using Lifee.Puller.RSS.Service.Dtos;

namespace Lifee.Puller.RSS.IntegrationCommands.Handlers;

public class TriggerRssFeedPullIntegrationCommandHandler : ICommandHandler<TriggerRssFeedPullIntegrationCommand, TriggerRssFeedPullIntegrationCommandResult>
{
    private readonly IRssFeedService _rssFeedService;
    
    public TriggerRssFeedPullIntegrationCommandHandler(IRssFeedService rssFeedService)
    {
        _rssFeedService = rssFeedService;
    }
    
    public async Task<object> Process(TriggerRssFeedPullIntegrationCommand command)
    {
        var result = await _rssFeedService.PullFeed(new PullRssFeedDto
        {
            RssFeedUuid = command.RssFeedUuid
        });

        return new TriggerRssFeedPullIntegrationCommandResult
        {
            IsSuccess = result.IsSuccess,
            SynchronizedItems = result.ValueOrDefault?.SynchronizedItems,
            Reasons = result.Reasons.Select(e => (e.Message, e.Metadata)).ToList()
        };
    }
}