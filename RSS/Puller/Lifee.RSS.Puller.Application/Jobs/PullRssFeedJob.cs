using Lifee.Puller.RSS.Service;
using Lifee.Puller.RSS.Service.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lifee.Puller.RSS.Jobs;

public class PullRssFeedJob
{

    private readonly IServiceProvider _serviceProvider;
    
    public PullRssFeedJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void Handle(Guid feedUuid, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return;
        
        using var serviceScope = _serviceProvider.CreateScope();

        var logger = _serviceProvider.GetService<ILogger<PullRssFeedJob>>();

        if (logger is null)
            return;
        
        using var loggerScope = logger.BeginScope(new Dictionary<string, object>
        {
            ["FeedUuid"] = feedUuid
        });

        using var rssFeedSynchronizerService = _serviceProvider.GetService<IRssFeedService>();

        if (rssFeedSynchronizerService is null)
            return;
        
        logger.LogInformation("Starting processing the job");
        
        rssFeedSynchronizerService.PullFeed(new PullRssFeedDto
        {
            RssFeedUuid = feedUuid
        });
        
        logger.LogInformation("Job processed successfully");
    }
    
}