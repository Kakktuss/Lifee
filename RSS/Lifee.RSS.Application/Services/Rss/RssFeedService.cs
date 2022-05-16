using FluentResults;
using Lifee.RSS.Application.DataAccess.Repositories;
using Lifee.RSS.Application.Models.RssFeed;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lifee.RSS.Application.Services;

public class RssFeedService : IRssFeedService
{
    private readonly IRssFeedRepository _rssFeedRepository;

    private readonly ILogger<RssFeedService> _logger;

    public RssFeedService(IRssFeedRepository rssFeedRepository,
        ILogger<RssFeedService> logger)
    {
        _rssFeedRepository = rssFeedRepository;

        _logger = logger;
    }

    public async Task<Result<CreateRssFeedResultDto>> CreateRssFeed(CreateRssFeedDto createRssFeedDto)
    {
        var rssFeed = new RssFeed
        {
            Url = createRssFeedDto.RssFeedUrl
        };
        
        _rssFeedRepository.AddRssFeed(rssFeed);
        
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
        
        return Result.Ok(new CreateRssFeedResultDto
        {
            RssFeedUuid = rssFeed.Uuid
        });
    }
}