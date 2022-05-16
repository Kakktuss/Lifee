using Hangfire;
using HotBrokerBus.Abstractions.Events;
using Lifee.Puller.RSS.DataAccess.Repositories;
using Lifee.Puller.RSS.IntegrationEvents.Events;
using Lifee.Puller.RSS.Jobs;
using Lifee.Puller.RSS.Service;
using Lifee.Puller.RSS.Service.Dtos;
using Microsoft.Extensions.Logging;

namespace Lifee.Puller.RSS.IntegrationEvents.Handlers;

public class RssFeedCreatedIntegrationEventHandler : IEventHandler<RssFeedCreatedIntegrationEvent>
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IRssFeedService _rssFeedService;
    
    private readonly ILogger<RssFeedCreatedIntegrationEventHandler> _logger;
    
    public RssFeedCreatedIntegrationEventHandler(IServiceProvider serviceProvider,
        IRssFeedService rssFeedService,
        ILogger<RssFeedCreatedIntegrationEventHandler> logger)
    {
        _serviceProvider = serviceProvider;

        _rssFeedService = rssFeedService;
        
        _logger = logger;
    }
    
    public async Task<bool> Handle(RssFeedCreatedIntegrationEvent @event)
    {
        RecurringJob.AddOrUpdate($"pull-rss-{@event.RssFeedUuid}", 
            () => new PullRssFeedJob(_serviceProvider).Handle(@event.RssFeedUuid, CancellationToken.None), 
            Cron.Hourly);
        
        var result = await _rssFeedService.PullFeed(new PullRssFeedDto
        {
            RssFeedUuid = @event.RssFeedUuid
        });

        if (result.IsFailed)
        {
            return false;
        }

        return true;
    }
}