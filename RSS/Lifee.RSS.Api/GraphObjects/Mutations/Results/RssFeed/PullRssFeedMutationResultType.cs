using HotChocolate.Types;
using Lifee.RSS.Api.GraphObjects.ObjectTypes;
using Lifee.RSS.Application.Services.Dtos.Results;

namespace Lifee.RSS.Api.GraphObjects.Mutations.Results.RssFeed;

public class PullUserRssFeedMutationResultType : ObjectType<PullUserRssFeedResultDto>
{
	protected override void Configure(IObjectTypeDescriptor<PullUserRssFeedResultDto> descriptor)
	{
		descriptor.BindFieldsImplicitly();

		descriptor.Field(e => e.SynchronizedRssFeedItems)
			.Type<ListType<RssFeedItemObjectType>>();
	}
}