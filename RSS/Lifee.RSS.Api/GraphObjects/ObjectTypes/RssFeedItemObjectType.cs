using HotChocolate.Types;
using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Api.GraphObjects.ObjectTypes;

public class RssFeedItemObjectType : ObjectType<RssFeedItem>
{
    protected override void Configure(IObjectTypeDescriptor<RssFeedItem> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Ignore(e => e.Id);

        descriptor.Ignore(e => e.FeedId);
    }
}