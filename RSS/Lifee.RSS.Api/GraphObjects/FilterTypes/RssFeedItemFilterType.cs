using HotChocolate.Data.Filters;
using Lifee.RSS.Application.Models.RssFeed;

namespace Lifee.RSS.Api.GraphObjects.FilterTypes;

public class RssFeedItemFilterType : FilterInputType<RssFeedItem>
{
    protected override void Configure(IFilterInputTypeDescriptor<RssFeedItem> descriptor)
    {
        descriptor.BindFieldsImplicitly();

        descriptor.Ignore(e => e.Id);

        descriptor.Ignore(e => e.FeedId);
    }
}