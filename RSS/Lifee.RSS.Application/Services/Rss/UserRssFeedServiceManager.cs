using FluentResults;
using Lifee.RSS.Application.DataAccess.Repositories;
using Lifee.RSS.Application.Models.RssFeed;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lifee.RSS.Application.Services;

public class UserRssFeedServiceManager : IUserRssFeedServiceManager
{

    private readonly IRssFeedRepository _rssFeedRepository;

    private readonly IUserRepository _userRepository;

    private readonly IRssFeedService _rssFeedService;

    private readonly ILogger<UserRssFeedServiceManager> _logger;

    public UserRssFeedServiceManager(IRssFeedRepository rssFeedRepository,
        IUserRepository userRepository,
        IRssFeedService rssFeedService,
        ILogger<UserRssFeedServiceManager> logger)
    {
        _rssFeedRepository = rssFeedRepository;

        _userRepository = userRepository;
        
        _rssFeedService = rssFeedService;

        _logger = logger;
    }

    public async Task<Result<CreateUserFeedResultDto>> CreateFeed(CreateUserFeedDto createUserFeedDto)
    {
        RssFeed? rssFeed;
        
        if (!await _rssFeedRepository.ExistsAsync(e => e.Url == createUserFeedDto.RssFeedUrl))
        {
            var rssFeedCreationResult = await _rssFeedService.CreateRssFeed(new CreateRssFeedDto
            {
                RssFeedUrl = createUserFeedDto.RssFeedUrl
            });

            if (rssFeedCreationResult.IsFailed)
            {
                return Result.Fail(new Error(""));
            }

            rssFeed = rssFeedCreationResult.Value.RssFeed;
        }
        else
        {
            rssFeed = await _rssFeedRepository.GetRssFeed(e => e.Url == createUserFeedDto.RssFeedUrl);
        }
        
        var user = await _userRepository.GetUserAsync(e => e.Uuid == createUserFeedDto.UserUuid);
        
        user?.AddRssFeed(rssFeed!);
        
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
        
        return Result.Ok(new CreateUserFeedResultDto
        {
            RssFeed = rssFeed!
        });
    }

}