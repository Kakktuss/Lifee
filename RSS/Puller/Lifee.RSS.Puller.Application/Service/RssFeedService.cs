using System.ServiceModel.Syndication;
using System.Xml;
using FluentResults;
using HotBrokerBus.Abstractions.Stan.Events;
using Lifee.Puller.RSS.DataAccess.Repositories;
using Lifee.Puller.RSS.IntegrationEvents.Events;
using Lifee.Puller.RSS.Models.RssFeed;
using Lifee.Puller.RSS.Service.Dtos;
using Lifee.Puller.RSS.Service.Dtos.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lifee.Puller.RSS.Service;

public class RssFeedService : IRssFeedService
{
    private readonly IRssFeedRepository _rssFeedRepository;

    private readonly IStanEventBusConsumerClient _stanEventBusConsumerClient;
    
    private readonly ILogger<RssFeedService> _logger;
    
    public RssFeedService(IRssFeedRepository rssFeedRepository,
        IStanEventBusConsumerClient stanEventBusConsumerClient,
        ILogger<RssFeedService> logger)
    {
        _rssFeedRepository = rssFeedRepository;

        _stanEventBusConsumerClient = stanEventBusConsumerClient;

        _logger = logger;
    }

    public async Task<Result<PullRssFeedResultDto>> PullFeed(PullRssFeedDto pullRssFeedDto)
    {
        _logger.LogInformation("Starting to pull the feed");

        var rssFeed = await _rssFeedRepository.GetRssFeed(e => e.Uuid == pullRssFeedDto.RssFeedUuid, 
            x => x.Include(
                e => e.Items));

        if (rssFeed is null)
        {
            _logger.LogError("Unable to find the requested feed");
            
            return Result.Fail(new Error("Unable to find the requested RssFeed").WithMetadata("errCode", "errRssFeedNotFound"));
        }
        
        var synchronizedFeedItems = PullFeedItems(in rssFeed);
        
        _rssFeedRepository.UpdateRssFeed(rssFeed);

        try
        {
            var result = await _rssFeedRepository.UnitOfWork.SaveAsync();

            if (!result)
            {
                _logger.LogError("Unable to save the changes to the database");

                return Result.Fail(new Error("Unable to save the RssFeed").WithMetadata("errCode", "errDbUpdate"));
            }
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError(exception, "Unable to save the changes to the database");
            
            return Result.Fail(new Error("Unable to update the RssFeed").WithMetadata("errCode", "errDbUpdate"));
        }

        _stanEventBusConsumerClient.Publish("rss.feed.synchronized", new RssFeedSynchronizedIntegrationEvent());
        
        _logger.LogInformation("Successfully updated the RssFeed");
        
        return Result.Ok(new PullRssFeedResultDto
        {
            SynchronizedItems = synchronizedFeedItems
        });
    }
    
    private List<RssFeedItem> PullFeedItems(in RssFeed rssFeed)
    {
        using XmlReader feedReader = XmlReader.Create(rssFeed.Url, new XmlReaderSettings { Async = true });

        SyndicationFeed syndicationFeed = SyndicationFeed.Load(feedReader);
        
        feedReader.Close();

        var lastFeedItemId = rssFeed.Items.LastOrDefault()?.ItemId ?? "";
        
        var synchronizedFeedItems = new List<RssFeedItem>();

        var syndicationFeedItemsList = syndicationFeed.Items.Reverse().ToList();
        
        var lastFeedItemIndex = syndicationFeedItemsList.FindIndex(e => e.Id == lastFeedItemId);

        foreach (SyndicationItem feedItem in syndicationFeedItemsList.Skip(lastFeedItemIndex != -1 ? lastFeedItemIndex + 1 : 0))
        {
            synchronizedFeedItems.Add(new RssFeedItem
            {
                Title = feedItem.Title.Text,
                Description = feedItem.Summary.Text,
                ItemId = feedItem.Id,
                Link = feedItem.Links.First().Uri.ToString()
            });
        }
        
        rssFeed.AddItems(synchronizedFeedItems);

        return synchronizedFeedItems;
    }

    private bool _disposed = false;
    
    public void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _rssFeedRepository.Dispose();
        }

        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }
}