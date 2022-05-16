using System.Security.Claims;
using HotChocolate;
using HotChocolate.Types;
using Lifee.RSS.Application.Services;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;

namespace Lifee.RSS.Api.GraphObjects.Mutations;

[ExtendObjectType(Name = "Mutations")]
public class RssFeedMutations
{
	[GraphQLName("pullUserRssFeed")]
	public async Task<PullUserRssFeedResultDto> PullRssFeed([Service] IRssFeedPullerService rssFeedPullerService,
		[Service] IHttpContextAccessor contextAccessor,
		PullUserRssFeedDto pullUserRssFeedDto)
	{
		if (contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value is null)
			throw new UnauthorizedAccessException("The user can't be found");

		pullUserRssFeedDto.UserUuid = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

		var result = await rssFeedPullerService.PullUserFeed(pullUserRssFeedDto);

		if (result.IsFailed)
		{
			throw new ApplicationException("");
		}

		return result.Value;
	}

	[GraphQLName("createUserRssFeed")]
	public Task CreateRssFeed()
	{
		
	}

	[GraphQLName("bindUserRssFeed")]
	public Task BindRssFeed()
	{
		
	}
}