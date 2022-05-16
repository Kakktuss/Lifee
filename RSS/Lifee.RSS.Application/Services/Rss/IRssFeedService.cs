using FluentResults;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;

namespace Lifee.RSS.Application.Services;

public interface IRssFeedService
{
    public Task<Result<CreateRssFeedResultDto>> CreateRssFeed(CreateRssFeedDto createRssFeedDto);
}