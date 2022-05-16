using HotChocolate.Types;
using Lifee.RSS.Application.Services;
using Lifee.RSS.Application.Services.Dtos;
using Lifee.RSS.Application.Services.Dtos.Results;

namespace Lifee.RSS.Api.GraphObjects.Mutations;

[ExtendObjectType(Name = "Mutations")]
public class RssFeedAdminMutations
{
	[GraphQLName("pullRssFeed")]
	public async Task<PullRssFeedResultDto> PullRssFeed([Service] IRssFeedPullerService rssFeedPullerService, 
		PullRssFeedDto pullRssFeedDto)
	{
		var result = await rssFeedPullerService.PullFeed(pullRssFeedDto);
		
		if (result.IsFailed)
		{
			throw new ApplicationException("");
		}

		return result.Value;
	}

	[GraphQLName("createRssFeed")]
	public Task CreateRssFeed([Service] I)
	{
		
	}

}